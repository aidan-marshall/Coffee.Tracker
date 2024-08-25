using CoffeeTracker.Repositories;

namespace CoffeeTracker.Services
{
    public class CoffeRecordService(CoffeeRecordRepository repository)
    {
        public int CreateCoffeRecord(CoffeeRecord coffeRecord) 
        {
            return repository.InsertCoffeeRecord(coffeRecord);
        }
    }
}
