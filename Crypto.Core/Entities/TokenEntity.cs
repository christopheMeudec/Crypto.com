namespace Crypto.Core.Entities;

public class TokenEntity
{
    public string TokenCode { get; set; }
    
    public ICollection<TokenExchangeHistoryEntity> ExchangeHistory { get;  } = new List<TokenExchangeHistoryEntity>();
    public ICollection<TokenValueHistoryEntity> ValueHistory { get;  } = new List<TokenValueHistoryEntity>();
}

