using CoffeeTracker;
using CoffeeTracker.Repositories;
using CoffeeTracker.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    return Results.Ok("Hello, World!");
});

app.MapPost("/coffee/create", (CoffeeRecord coffeeRecord) => {
    
    var connection = new NpgsqlConnection(connectionString: "Host=localhost;Port=5432;Username=postgres;Password=random;Database=CoffeeTracker;");
    var coffeRecordRepository = new CoffeeRecordRepository(connection);
    var coffeRecordService = new CoffeRecordService(coffeRecordRepository);

    var recordId = coffeRecordService.CreateCoffeRecord(coffeeRecord);

    return Results.Ok(recordId);

    });

app.Run();
