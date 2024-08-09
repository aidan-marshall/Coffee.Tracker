namespace CoffeeTracker
{
    public class CoffeeRecord
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required DateTime TimeOfConsumption { get; set; }
        public required string CoffeeType { get; set; }
        public required string Location { get; set; }
    }
}
