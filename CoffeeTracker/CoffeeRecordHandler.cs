using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker;

public class CoffeeRecordHandler(CoffeeTrackerContext context, ILogger<CoffeeRecordHandler> logger)
{
    public async Task<CoffeeRecord> InsertCoffeeRecordAsync(CoffeeRecord record)
    {
        try
        {
            if (record.TimeOfConsumption.Kind != DateTimeKind.Utc)
            {
                record.TimeOfConsumption = record.TimeOfConsumption.ToUniversalTime();
            }

            var result = context.Records.Add(record);
            await context.SaveChangesAsync();
            
            return result.Entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while inserting a coffee record.");
            throw;
        }
    }
    
    public async Task<CoffeeRecord> GetCoffeeRecordAsync(int id)
    {
        try
        {
            var result = await context.Records.FindAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Record with id {id} not found.");
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving coffee record with id: {Id}.", id);
            throw;
        }
    }
    
    public async Task<IEnumerable<CoffeeRecord>> GetCoffeeRecordsAsync()
    {
        try
        {
            return await context.Records.ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all coffee records.");
            throw;
        }
    }
}