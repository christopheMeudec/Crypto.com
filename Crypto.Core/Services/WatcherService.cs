using Crypto.Core.Repositories;

namespace Crypto.Core.Services;

public class WatcherService(IDataRepository dataRepository, INotificationService notificationService)
    : IWatcherService
{
    public async Task CheckChanges(CancellationToken cancellationToken)
    {
        foreach (var currentCoin in await dataRepository.GetCoins(cancellationToken))
        {
            var variation = 100 - (currentCoin.CurrentValue * 100 / currentCoin.PurchaseValue);

            if (variation is >= 10 or <= 10)
            {
                var message = $"[{variation}%] Coin {currentCoin.TokenCode} has changed value from {currentCoin.PurchaseValue} to {currentCoin.CurrentValue}";
                await notificationService.Notify(message, cancellationToken);
            }

        }
    }
}

public interface IWatcherService
{
    Task CheckChanges(CancellationToken cancellationToken);
}