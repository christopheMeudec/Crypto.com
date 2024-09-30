using Crypto.Core.Entities;
using Crypto.Core.Repositories;

namespace Crypto.Core.Services;

public class DataCollectorService(IDataRepository dataRepository, ICryptoService cryptoService)
    : IDataCollectorService
{
    public async Task CollectDataAsync(CancellationToken cancellationToken)
    {
        foreach (var currentCoin in await dataRepository.GetTokens(cancellationToken))
        {
            var valuations = await cryptoService.GeValuations(currentCoin.TokenCode, 5, cancellationToken);

            var tokenValueHistory = valuations.Select(
                c => new TokenValueHistoryEntity
                {
                    TokenCode = currentCoin.TokenCode,
                    RecordedDate = c.Timestamp,
                    Value = c.Value
                }).ToList();

            await dataRepository.AddTokenValues(tokenValueHistory, cancellationToken);
        }
    }
}

public interface IDataCollectorService
{
    Task CollectDataAsync(CancellationToken cancellationToken);
}