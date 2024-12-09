using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker;

public class CoffeeTrackerContext(DbContextOptions<CoffeeTrackerContext> options) : DbContext(options)
{
    public DbSet<CoffeeRecord> Records { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoffeeRecord>(entity =>
        {
            entity.ToTable("records");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.TimeOfConsumption).HasColumnName("time_of_consumption").IsRequired();
            entity.Property(e => e.CoffeeType).HasColumnName("coffee_type").IsRequired();
            entity.Property(e => e.Location).HasColumnName("location").IsRequired();
        });
    }
}