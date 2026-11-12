using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using uChat_API;
using uChat_API.DbModel;
using uChat_API.Websocket;

// Startup Init
Console.WriteLine("[Info] uChat is now starting, please wait.");
string? AppBasePath = AppContext.BaseDirectory;

if (AppBasePath == null) {
    Console.WriteLine("[Error] Application base path is no accessible.");
    Console.ReadKey(true);
    return;
}

string? ConnectionStringPath = Path.Combine(AppBasePath, "ConnStr.ucdata");
if (!Path.Exists(ConnectionStringPath)) {
    Console.WriteLine($"[Error] Connection string properties file is not exists.\n" +
        $"Please create it at {ConnectionStringPath}");
    Console.ReadKey(true);
    return;
}

string[] propVerified = { "ConnStr", "Database" };
string[] connFileCtx = File.ReadAllText(ConnectionStringPath).Trim().Split("\n");
Dictionary<string, string> ConnStrContext = new Dictionary<string, string>();
foreach (string line in connFileCtx) {
    string[] ctx = line.Split("=>");

    if (ctx.Length != 2) {
        Console.WriteLine("[Error] Wrong connection string properties format.");
        Console.ReadKey(true);
        return;
    }

    ConnStrContext[ctx[0]] = ctx[1];
}

if (!propVerified.All(ConnStrContext.ContainsKey)) {
    Console.WriteLine("[Error] Wrong connection string properties format.");
    Console.ReadKey(true);
    return;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Test if server is connectable
try {
    using var sqlConn = new SqlConnection(ConnStrContext["ConnStr"]);
    sqlConn.Open();
}
catch (Exception ex) {
    Console.WriteLine($"[Error] Cannot open connection to the SQL Server.");
    Console.ReadKey(true);
    return;
}

string ConnectionString;

// Test if target db is connectable
try {
    ConnectionString = ConnStrContext["ConnStr"] + ConnStrContext["Database"];
    using var sqlConn = new SqlConnection(ConnectionString);
    sqlConn.Open();
}
catch (Exception ex) {
    Console.WriteLine($"[Info] Target database is not exists. Creating now.");

    if (DbBase.TryCreateDb(ConnStrContext["ConnStr"])) {
        Console.WriteLine("[Info] Database created. Please re-open server again.");
    }
    else {
        Console.WriteLine("[Error] Failed to create database. Please re-open server again.");
    }

    
    Console.ReadKey(true);
    return;
}

builder.WebHost.UseUrls("http://0.0.0.0:5331");

builder.Services.AddDbContext<UChatDbContext>(options =>
        options.UseSqlServer(ConnectionString));

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