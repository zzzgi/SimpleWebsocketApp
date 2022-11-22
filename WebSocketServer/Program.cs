using System.Net.WebSockets;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseWebSockets();

app.Use(async (context, next) =>
{

    if (context.WebSockets.IsWebSocketRequest)
    {
        WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
        Console.WriteLine("Websocket Connected.");
    }

    else
    {
        await next();
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
