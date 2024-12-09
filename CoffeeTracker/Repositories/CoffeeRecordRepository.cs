using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Repositories;

public class CoffeeRecordRepository(CoffeeTrackerContext context)
{
    public async Task<int> InsertCoffeeRecordAsync(CoffeeRecord record)
    {
        context.Records.Add(record);
        await context.SaveChangesAsync();
        return record.Id;
    }
    
    public async Task<CoffeeRecord> GetCoffeeRecordAsync(int id)
    {
        var result = await context.Records.FindAsync(id);
        
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        
        return result;
    }
    
    public async Task<IEnumerable<CoffeeRecord>> GetCoffeeRecordsAsync()
    {
        return await context.Records.ToListAsync();
    }
}