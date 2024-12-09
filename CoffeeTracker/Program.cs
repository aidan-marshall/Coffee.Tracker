using CoffeeTracker;
using CoffeeTracker.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("COFFEE_TRACKER_CONNECTION_STRING");

builder.Services.AddDbContext<CoffeeTrackerContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<CoffeeRecordRepository>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Hello, World!"));

app.MapPost("/coffee", async (CoffeeRecord coffeeRecord, CoffeeRecordRepository repository) =>
{
    var recordId = await repository.InsertCoffeeRecordAsync(coffeeRecord);
    return Results.Ok(recordId);
});

app.MapGet("/coffee", async (CoffeeRecordRepository repository) =>
{
    var records = await repository.GetCoffeeRecordsAsync();
    return Results.Ok(records);
});

app.MapGet("coffee/{id}", async (int id, CoffeeRecordRepository repository) =>
{
    try
    {
        var record = await repository.GetCoffeeRecordAsync(id);
        return Results.Ok(record);
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
});

app.Run();