using Crypto.Core.Entities;
using Crypto.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Core.Repositories;

public class DataRepository(DataContext dataContext) : IDataRepository
{
    public async Task<IEnumerable<TokenEntity>> GetTokens(CancellationToken cancellationToken)
    {
        return await dataContext.Tokens
            .Include(b => b.ValueHistory)
            .Include(b => b.ExchangeHistory)
            .ToListAsync(cancellationToken);
    }

    public async Task AddTokenValues(List<TokenValueHistoryEntity> tokenHistory, CancellationToken cancellationToken)
    {
        foreach (var value in tokenHistory)
        {
            if (!await dataContext.TokensValueHistory.AnyAsync(c => c.TokenCode == value.TokenCode && c.RecordedDate == value.RecordedDate, cancellationToken))
            {
                await dataContext.TokensValueHistory.AddAsync(value, cancellationToken);
            }
        }
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}

public interface IDataRepository
{
    Task<IEnumerable<TokenEntity>> GetTokens(CancellationToken cancellationToken);
    Task AddTokenValues(List<TokenValueHistoryEntity> tokenHistory, CancellationToken cancellationToken);
}
