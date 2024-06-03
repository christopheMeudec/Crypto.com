using Crypto.Core.Entities;
using Crypto.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Core.Repositories;

public class DataRepository(DataContext dataContext) : IDataRepository
{
    public async Task<IEnumerable<TokenEntity>> GetCoins(CancellationToken cancellationToken)
    {
        return await dataContext.Tokens.ToListAsync(cancellationToken);
    }

    public async Task AddCoinValue(string instrumentationName, decimal currentValue, CancellationToken cancellationToken)
    {
        await dataContext.TokensHistory.AddAsync(new TokenHistoryEntity
        {
            TokenCode = instrumentationName,
            RecordedDate = DateTime.Now,
            Value = currentValue
        }, cancellationToken);

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}

public interface IDataRepository
{
    Task<IEnumerable<TokenEntity>> GetCoins(CancellationToken cancellationToken);
    Task AddCoinValue(string instrumentationName, decimal currentValue, CancellationToken cancellationToken);
}
