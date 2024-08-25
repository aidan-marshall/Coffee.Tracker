using CoffeeTracker;
using CoffeeTracker.Repositories;
using CoffeeTracker.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    return Results.Ok("Hello, World!");
});

app.MapGet("/coffee/create", () => {

    var coffeeRecord = new CoffeeRecord
    {
        UserId = 123,
        TimeOfConsumption = DateTime.Now,
        CoffeeType = "Espresso",
        Location = "Office"
    };
    
    var connection = new NpgsqlConnection(connectionString: "Host=localhost;Port=5432;Username=postgres;Password=random;Database=CoffeeTracker;");
    var coffeRecordRepository = new CoffeeRecordRepository(connection);
    var coffeRecordService = new CoffeRecordService(coffeRecordRepository);

    var recordId = coffeRecordService.CreateCoffeRecord(coffeeRecord);

    return Results.Ok(recordId);
    });

app.Run();
