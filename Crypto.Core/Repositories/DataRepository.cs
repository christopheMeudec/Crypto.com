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

    public async Task AddTokenValue(string tokenCode, decimal currentValue, CancellationToken cancellationToken)
    {
        await dataContext.TokensValueHistory.AddAsync(new TokenValueHistoryEntity
        {
            TokenCode = tokenCode,
            RecordedDate = DateTime.Now,
            Value = currentValue
        }, cancellationToken);

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}

public interface IDataRepository
{
    Task<IEnumerable<TokenEntity>> GetCoins(CancellationToken cancellationToken);
    Task AddTokenValue(string tokenCode, decimal currentValue, CancellationToken cancellationToken);
}
