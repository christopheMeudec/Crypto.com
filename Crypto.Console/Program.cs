using Crypto.Console;
using Crypto.Core.Settings;
using Crypto.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("** Here We go!**");

var host = new HostBuilder()
          .ConfigureHostConfiguration(configHost =>
          {
          })
          .ConfigureLogging((hostContext, configLogging) =>
          {
              configLogging.AddConsole();
          })
          .ConfigureServices((hostContext, 
          services) =>
          {
              services
                .AddSingleton<CryptoSettings>(sp => new CryptoSettings("https://uat-api.3ona.co/", "", ""))
                .AddScoped<ICryptoService, CryptoService>();

              services.AddHostedService<TimedHostedService>();
              services.AddLogging();
          })
         .UseConsoleLifetime()
         .Build();

//run the host
await host.RunAsync();
