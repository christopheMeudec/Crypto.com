using Crypto.Console;
using Crypto.Console.Extensions;
using Crypto.Core.Persistence;
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
        var cryptoSettings = hostContext.Configuration.GetSection("CryptoSettings").Get<CryptoSettings>();
        services.AddSingleton<CryptoSettings>(cryptoSettings);

        var telegramSettings = hostContext.Configuration.GetSection("TelegramSettings").Get<TelegramSettings>();
        services.AddSingleton<TelegramSettings>(telegramSettings);

        services.AddScoped<DataContext>();
        services.AddScoped<IDataRepository, DataRepository>();

        services.AddHttpClient<ICryptoService, CryptoService>();

        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<IDataCollectorService, DataCollectorService>();
        services.AddScoped<INotificationService, TelegramNotificationService>();
        services.AddScoped<IWatcherService, WatcherService>();

        services.AddHostedService<TimedHostedService>();
        services.AddLogging();
    })
    .UseConsoleLifetime()
    .Build();

//run the host
await host
    .SeedData()
    .RunAsync();