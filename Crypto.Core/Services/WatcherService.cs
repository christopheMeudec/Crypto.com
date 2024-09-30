using Crypto.Core.Entities;
using Crypto.Core.Repositories;

namespace Crypto.Core.Services;

public class WatcherService(IDataRepository dataRepository, INotificationService notificationService)
    : IWatcherService
{
    public async Task CheckChanges(CancellationToken cancellationToken)
    {
        foreach (var currentToken in await dataRepository.GetTokens(cancellationToken))
        {
            if (!currentToken.ValueHistory.Any() || !currentToken.ExchangeHistory.Any())
                continue;

            var currentValue = currentToken.ValueHistory.MaxBy(c => c.RecordedDate)!.Value;
            var tokenExchangeHistoryEntity = currentToken.ExchangeHistory.MaxBy(c => c.RecordedDate);

            var variation = Math.Round(100 - (currentValue * 100 / tokenExchangeHistoryEntity!.Value), 2);

            switch (variation)
            {
                case >= 2 when tokenExchangeHistoryEntity.ExchangeType == ExchangeTypeEnum.Buy:
                    {
                        var message = $"[{variation}%] Token {currentToken.TokenCode} has changed value from {tokenExchangeHistoryEntity.Value} to {currentValue}";
                        await notificationService.Notify(message, cancellationToken);
                        break;
                    }
                case <= 2 when tokenExchangeHistoryEntity.ExchangeType == ExchangeTypeEnum.Sell:
                    {
                        var message = $"[{variation}%] Token {currentToken.TokenCode} has changed value from {tokenExchangeHistoryEntity.Value} to {currentValue}";
                        await notificationService.Notify(message, cancellationToken);
                        break;
                    }
            }
        }
    }
}

public interface IWatcherService
{
    Task CheckChanges(CancellationToken cancellationToken);
}