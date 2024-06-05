using Crypto.Core.Entities;
using Crypto.Core.Repositories;

namespace Crypto.Core.Services;

public class WatcherService(IDataRepository dataRepository, INotificationService notificationService)
    : IWatcherService
{
    public async Task CheckChanges(CancellationToken cancellationToken)
    {
        foreach (var currentCoin in await dataRepository.GetCoins(cancellationToken))
        {
            if (!currentCoin.ValueHistory.Any() || !currentCoin.ExchangeHistory.Any())
                continue;

            var currentValue = currentCoin.ValueHistory.MaxBy(c => c.RecordedDate)!.Value;
            var tokenExchangeHistoryEntity = currentCoin.ExchangeHistory.MaxBy(c =>c.RecordedDate);

            var variation = 100 - (currentValue * 100 / tokenExchangeHistoryEntity!.Value);

            if (variation is >= 10 && tokenExchangeHistoryEntity.ExchangeType == ExchangeTypeEnum.Buy)
            {
                var message = $"[{variation}%] Coin {currentCoin.TokenCode} has changed value from {tokenExchangeHistoryEntity.Value} to {currentValue}";
                await notificationService.Notify(message, cancellationToken);
            }
            else if (variation is <= 10 && tokenExchangeHistoryEntity.ExchangeType == ExchangeTypeEnum.Sell)
            {
                var message = $"[{variation}%] Coin {currentCoin.TokenCode} has changed value from {tokenExchangeHistoryEntity.Value} to {currentValue}";
                await notificationService.Notify(message, cancellationToken);
            }
        }
    }
}

public interface IWatcherService
{
    Task CheckChanges(CancellationToken cancellationToken);
}