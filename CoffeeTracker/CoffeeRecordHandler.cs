using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker;

public class CoffeeRecordHandler(CoffeeTrackerContext context)
{
    public async Task<CoffeeRecord> InsertCoffeeRecordAsync(CoffeeRecord record)
    {
        record.TimeOfConsumption = record.TimeOfConsumption.ToUniversalTime();
    
        var result = context.Records.Add(record);
        await context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<CoffeeRecord> GetCoffeeRecordAsync(int id)
    {
        var result = await context.Records.FindAsync(id);
        
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with id {id} not found.");
        }
        
        return result;
    }
    
    public async Task<IEnumerable<CoffeeRecord>> GetCoffeeRecordsAsync()
    {
        return await context.Records.ToListAsync();
    }
}