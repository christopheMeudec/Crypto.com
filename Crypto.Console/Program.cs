using Crypto.Console;
using Crypto.Core.Repositories;
using Crypto.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Crypto.Core.Services;
using Microsoft.Extensions.Configuration;

Console.WriteLine("** Here We go!**");

var host = new HostBuilder()
    .ConfigureHostConfiguration(configHost =>
    {
        configHost.AddJsonFile("appsettings.json");
    })
    .ConfigureLogging((hostContext, configLogging) =>
    {
        configLogging.AddConsole();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<CryptoSettings>(sp => new CryptoSettings("https://uat-api.3ona.co/", "", ""));

        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<IDataRepository, DataRepository>();
        services.AddScoped<IDataCollectorService, DataCollectorService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IWatcherService, WatcherService>();

        services.AddHostedService<TimedHostedService>();
        services.AddLogging();
    })
    .UseConsoleLifetime()
    .Build();

//run the host
await host.RunAsync();
