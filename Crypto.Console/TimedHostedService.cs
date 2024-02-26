using Crypto.Core;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace Crypto.Console;

public class TimedHostedService : IHostedService, IDisposable
{
    private Timer _timer;
    private Task _executingTask;
    private double expectedValue;
    private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
    private readonly ICryptoService _cryptoService;

    public TimedHostedService(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // _logger.LogInformation("Timed Background Service is starting.");
        _timer = new Timer(ExecuteTask, null, TimeSpan.FromSeconds(30), TimeSpan.FromMilliseconds(-1));
        return Task.CompletedTask;
    }

    private void ExecuteTask(object state)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _executingTask = ExecuteTaskAsync(_stoppingCts.Token);
    }

    private async Task ExecuteTaskAsync(CancellationToken cancellationToken)
    {
        var valuation = await _cryptoService.GeValuations("CKBUSD-INDEX", 25, cancellationToken);
        var currentValue = double.Parse(valuation.OrderByDescending(o => o.Timestamp).First().Value, CultureInfo.InvariantCulture);

        if (currentValue > expectedValue)
        {
            // Alert
        }

        _timer.Change(TimeSpan.FromMinutes(2), TimeSpan.FromMilliseconds(-1));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // _logger.LogInformation("Timed Background Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        // Stop called without start
        if (_executingTask == null)
        {
            return;
        }
        try
        {
            // Signal cancellation to the executing method
            _stoppingCts.Cancel();
        }
        finally
        {
            // Wait until the task completes or the stop token triggers
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
    public void Dispose()
    {
        _stoppingCts.Cancel();
        _timer?.Dispose();
    }
}
