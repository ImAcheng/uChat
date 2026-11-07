using Microsoft.EntityFrameworkCore;
using uChat_API.DbModel;
using uChat_API.Websocket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UChatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));

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