using Crypto.Core;
using Crypto.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crypto.WinUI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        using IHost host = CreateHost();
        await host.StartAsync();

        IHostApplicationLifetime lifetime =
            host.Services.GetRequiredService<IHostApplicationLifetime>();

        using (IServiceScope scope = host.Services.CreateScope())
        {
            var mainForm = scope.ServiceProvider.GetRequiredService<MainMenuForm>();

            Application.Run(mainForm);
        }

        lifetime.StopApplication();
        await host.WaitForShutdownAsync();
    }

    private static IHost CreateHost()
    {
        string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services
                .AddSingleton<MainMenuForm>();

        builder.Services
                .AddSingleton<CryptoSettings>(sp => new CryptoSettings("https://uat-api.3ona.co/", "", ""))
                .AddScoped<ICryptoService, CryptoService>();

        return builder.Build();
    }

}