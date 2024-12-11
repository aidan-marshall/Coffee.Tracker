using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeTracker.Migrations;

public class CoffeeTrackerContextFactory : IDesignTimeDbContextFactory<CoffeeTrackerContext>
{
    public CoffeeTrackerContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<CoffeeTrackerContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CoffeeTrackerContext(optionsBuilder.Options);
    }
}