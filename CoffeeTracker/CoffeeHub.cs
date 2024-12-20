using Microsoft.AspNetCore.SignalR;

namespace CoffeeTracker;

public class CoffeeHub(CoffeeTrackerContext dbContext) : Hub
{
}