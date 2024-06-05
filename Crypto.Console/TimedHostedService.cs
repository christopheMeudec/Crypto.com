using Crypto.Core.Services;
using Microsoft.Extensions.Hosting;

namespace Crypto.Console;

public class TimedHostedService : IHostedService, IDisposable
{
    private Timer _timerDataCollector;
    private Task _executingTaskDataCollector;

    private Timer _timerWatcher;
    private Task _executingTaskWatcher;

    private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
    private readonly IDataCollectorService _dataCollectorService;
    private readonly IWatcherService _watcherService;

    public TimedHostedService(IDataCollectorService dataCollectorService, IWatcherService watcherService)
    {
        _dataCollectorService = dataCollectorService;
        _watcherService = watcherService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timerDataCollector = new Timer(ExecuteDataCollectorTask, null, TimeSpan.FromSeconds(60), TimeSpan.FromMilliseconds(-1));
        _timerWatcher = new Timer(ExecuteTaskWatcher, null, TimeSpan.FromMinutes(5), TimeSpan.FromMilliseconds(-1));
        return Task.CompletedTask;
    }

    private void ExecuteDataCollectorTask(object state)
    {
        _timerDataCollector?.Change(Timeout.Infinite, 0);
        _executingTaskDataCollector = ExecuteTaskDataCollectorAsync(_stoppingCts.Token);
    }

    private void ExecuteTaskWatcher(object state)
    {
        _timerWatcher?.Change(Timeout.Infinite, 0);
        _executingTaskWatcher = ExecuteTaskWatcherAsync(_stoppingCts.Token);
    }

    private async Task ExecuteTaskWatcherAsync(CancellationToken cancellationToken)
    {
        await _watcherService.CheckChanges(cancellationToken);

        _timerWatcher.Change(TimeSpan.FromMinutes(2), TimeSpan.FromMilliseconds(-1));
    }

    private async Task ExecuteTaskDataCollectorAsync(CancellationToken cancellationToken)
    {
        await _dataCollectorService.CollectDataAsync(cancellationToken);

        _timerDataCollector.Change(TimeSpan.FromMinutes(2), TimeSpan.FromMilliseconds(-1));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // _logger.LogInformation("Timed Background Service is stopping.");
        _timerDataCollector?.Change(Timeout.Infinite, 0);
        _timerWatcher?.Change(Timeout.Infinite, 0);

        // Stop called without start
        if (_executingTaskDataCollector == null && _executingTaskWatcher == null)
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
            await Task.WhenAny(_executingTaskDataCollector, _executingTaskWatcher, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }

    public void Dispose()
    {
        _stoppingCts.Cancel();
        _timerDataCollector?.Dispose();
        _timerWatcher?.Dispose();
    }
}
