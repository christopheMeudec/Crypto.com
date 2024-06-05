using Crypto.Core.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Crypto.Console.Extensions;

internal static class WebHostExtensions
{
    public static IHost SeedData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            var seeder = new DbInitializer(context);
            seeder.Seed();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred initializing the DB.");
        }

        return host;
    }
}