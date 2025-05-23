using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace open_telemetry_aspire.Endpoints;

interface IGreetings
{
    public Task<string> SendGreeting();
    public Task AddToCache(User request);

}
public class Greetings(ILogger<Greetings> logger, AppDbContext context): IGreetings
{
    const string OtelExample = "OTel.Example";
    const string Version = "1.0.0";
        
    public Task<string> SendGreeting()
    {
        // Custom metrics for the application
        var greetingsCount = "greetings.count";
        var countsTheNumberOfGreetings = "Counts the number of greetings";
        var greeteractivity = "GreeterActivity";

        var countGreetings = CreateCounter(greetingsCount, countsTheNumberOfGreetings);
        using var activity = CreateActivity(greeteractivity);

        logger.LogInformation("Sending greeting");
        countGreetings.Add(1);
        activity?.SetTag("greeting", "Hello World!");
        return Task.FromResult("Hello World!");
    }

    public async Task AddToCache(User request)
    {
        // Custom metrics for the application
        const string addToUsersCount = "addToUsers.count";
        var countGreetings = CreateCounter(addToUsersCount, "Adds a user to the users table");
        
        using var activity = CreateActivity("AddToCacheActivity");
        countGreetings.Add(1);
        logger.LogInformation(new EventId(1000),"Attempting to add user.");
        if(request.Name.Length > 10)
            logger.LogWarning(new EventId(1002), "Name length exceeded 15 chars.");
        if(request.Surname.Length > 10)
            logger.LogWarning(new EventId(1004), "Surname length exceeded 15 chars.");

        try
        {
            countGreetings.Add(1);
            await context.Persons.AddAsync(request);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogCritical(new EventId(1007), $"Failed to add user due to {ex.Message}");
        }
   
        await Task.FromResult(true);
    }

    private static Activity? CreateActivity(string activityName)
    {
        Activity? activity = null;
        try
        {
            // Custom ActivitySource for the application
            var greeterActivitySource = new ActivitySource(OtelExample);
            // Create a new Activity scoped to the method
            activity = greeterActivitySource.StartActivity(activityName);
            return activity;
        }
        catch
        {
            activity?.Dispose();
            throw;
        }
    }

    private static Counter<int> CreateCounter(string greetingsCount, string countsTheNumberOfGreetings)
    {
        var greeterMeter = new Meter(OtelExample, Version);
        var countGreetings = greeterMeter.CreateCounter<int>(greetingsCount, description: countsTheNumberOfGreetings);
        return countGreetings;
    }
}