using CoffeeTracker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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

app.MapGet("/", () => Results.Ok("Welcome to Coffee Tracker!"));

app.MapPost("/coffee", async (CoffeeRecord coffeeRecord, CoffeeRecordHandler dbHandler) =>
{
    try
    {
        var record = await dbHandler.InsertCoffeeRecordAsync(coffeeRecord);
        return Results.Ok(record);
    }
    catch (Exception)
    {
        return Results.Problem("Internal server error.");
    }
});

app.MapGet("/coffee", async (CoffeeRecordHandler dbHandler) =>
{
    try
    {
        var records = await dbHandler.GetCoffeeRecordsAsync();
        return Results.Ok(records);
    }
    catch (Exception)
    {
        return Results.Problem("Internal server error.");
    }
});

app.MapGet("coffee/{id:int}", async (int id, CoffeeRecordHandler dbHandler) =>
{
    try
    {
        var record = await dbHandler.GetCoffeeRecordAsync(id);
        return Results.Ok(record);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception)
    {
        return Results.Problem("Internal server error.");
    }
});

app.Run();