using System.Globalization;
using Crypto.Core.Repositories;

namespace Crypto.Core.Services;

public class DataCollectorService(IDataRepository dataRepository, ICryptoService cryptoService)
    : IDataCollectorService
{
    public async Task CollectDataAsync(CancellationToken cancellationToken)
    {
        foreach (var currentCoin in await dataRepository.GetCoins(cancellationToken))
        {
            var valuation = await cryptoService.GeValuations(currentCoin.TokenCode, 5, cancellationToken);
            var currentValue = decimal.Parse(valuation.OrderByDescending(o => o.Timestamp).First().Value, CultureInfo.InvariantCulture);

            await dataRepository.AddTokenValue(currentCoin.TokenCode, currentValue, cancellationToken);
        }
    }
}

public interface IDataCollectorService
{
    Task CollectDataAsync(CancellationToken cancellationToken);
}