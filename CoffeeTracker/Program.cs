using CoffeeTracker;
using CoffeeTracker.Repositories;
using CoffeeTracker.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("COFFEE_TRACKER_CONNECTION_STRING");
builder.Services.AddTransient<CoffeeRecordRepository>(
    _ =>
    {
        var connection = new NpgsqlConnection(connectionString);
        return new CoffeeRecordRepository(connection);
    });
builder.Services.AddTransient<CoffeRecordService>();

var app = builder.Build();

app.MapGet("/", () => {
    return Results.Ok("Hello, World!");
});

app.MapPost("/coffee", (CoffeeRecord coffeeRecord) => {
    var coffeeRecordService = app.Services.GetRequiredService<CoffeRecordService>();
    var recordId = coffeeRecordService.CreateCoffeRecord(coffeeRecord);

    return Results.Ok(recordId);
    });

app.Run();