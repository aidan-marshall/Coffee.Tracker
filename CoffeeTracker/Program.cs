using CoffeeTracker.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    Console.WriteLine("Hello World");
    var connection = new NpgsqlConnection();
});

app.Run();
