using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uChat_API;
using uChat_API.DbModel;
using uChat_API.Helper;
using uChat_API.Websocket;

// Startup Init
uPrint.Info(true, "uChat is now starting, please wait");
string? AppBasePath = AppContext.BaseDirectory;

// check base path
if (AppBasePath == null) {
    uPrint.Error(true, "Application base path is not accessible");
    Console.ReadKey(true);
    return;
}

// check properties file
string? PropertiesFilePath = Path.Combine(AppBasePath, "properties", "server.ucprop");
if (!Path.Exists(PropertiesFilePath)) {
    uPrint.Info(true, "Initializing server properties");

    try {
        Directory.CreateDirectory(Path.Combine(AppBasePath, "properties"));
        File.Create(PropertiesFilePath).Close();
    }
    catch {
        uPrint.Error(true, "Cannot initialize properties");
        Console.ReadKey(true);
    }

    bool _is = await PropertiesManager.Init(PropertiesFilePath);

    if (!_is) {
        uPrint.Error(true, "Cannot initialize properties");
        Console.ReadKey(true);
    }

    uPrint.Warning(true, $"uChat Server was just initialized. Please configure server settings in {PropertiesFilePath}");
    Console.ReadKey(true);
    return;
}

bool propChk = await PropertiesManager.Check(PropertiesFilePath);
if (!propChk) {
    uPrint.Info(true, "Initializing server properties");

    bool _is = await PropertiesManager.Init(PropertiesFilePath);

    if (!_is) {
        uPrint.Error(true, "Cannot initialize properties");
        Console.ReadKey(true);
    }

    uPrint.Warning(true, $"uChat Server was just initialized. Please configure server settings in {PropertiesFilePath}");
    Console.ReadKey(true);
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Test if server is connectable
var prop = PropertiesManager.Properties;
string svrConnStr = $"Data Source={prop["dbDataSource"]};" +
                $"Integrated Security={prop["dbIntegratedSecurity"]};" +
                $"Encrypt={prop["dbEncrypt"]};" +
                $"TrustServerCertificate={prop["dbTrustServerCertificate"]};";
try {
    using var sqlConn = new SqlConnection(svrConnStr);
    sqlConn.Open();
}
catch (Exception ex) {
    uPrint.Error(true, $"Cannot open connection to the SQL Server ({ex.Message})");
    Console.ReadKey(true);
    return;
}

// Test if target db is connectable
string dbConnStr = svrConnStr + $"Database={prop["databaseName"]}";
try {
    using var sqlConn = new SqlConnection(dbConnStr);
    sqlConn.Open();
}
catch (Exception) {
    uPrint.Info(true, "Target database is not exists. Creating now");

    if (DbBase.TryCreateDb(svrConnStr, prop["databaseName"])) {
        uPrint.Info(true, "Database created. Please re-open server again");
    }
    else {
        uPrint.Error(true, $"Failed to create database. Please re-open server again");
    }

    
    Console.ReadKey(true);
    return;
}

builder.WebHost.UseUrls($"http://{prop["targetIp"]}:{prop["port"]}");

builder.Services.AddDbContext<UChatDbContext>(options => {
    options.UseSqlServer(dbConnStr);
    options.LogTo(_ => { });
});

// this stops EntityFrameworkCore from printing messages
// builder.Logging.AddFilter(
//     "Microsoft.EntityFrameworkCore.Database.Command",
//     LogLevel.None
// );

builder.Services.AddSingleton<AccountHandler>();
builder.Services.AddSingleton<ChatHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseWebSockets();

app.MapControllers();

app.Map("api/ws/account", async context => {
    var handler = context.RequestServices
        .GetRequiredService<AccountHandler>();

    await handler.HandleAsync(context);
});

app.Map("api/ws/chat", async context => {
    var handler = context.RequestServices
        .GetRequiredService<ChatHandler>();

    await handler.HandleAsync(context);
});

app.Run();