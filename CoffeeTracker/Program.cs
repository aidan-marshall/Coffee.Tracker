using CoffeeTracker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("COFFEE_TRACKER_CONNECTION_STRING");

builder.Services.AddDbContext<CoffeeTrackerContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<CoffeeRecordHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => Results.Ok("Hello, World!"));

app.MapPost("/coffee", async (CoffeeRecord coffeeRecord, CoffeeRecordHandler repository) =>
{
    try
    {
        var recordId = await repository.InsertCoffeeRecordAsync(coffeeRecord);
        return Results.Ok(recordId);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
});

app.MapGet("/coffee", async (CoffeeRecordHandler repository) =>
{
    try
    {
        var records = await repository.GetCoffeeRecordsAsync();
        return Results.Ok(records);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("coffee/{id}", async (int id, CoffeeRecordHandler repository) =>
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
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();