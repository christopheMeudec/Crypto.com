namespace Crypto.Core.Entities;

public class TokenEntity
{
    public required string TokenCode { get; set; }
    public decimal PurchaseValue { get; set; }
    public decimal CurrentValue => History.MaxBy(h => h.RecordedDate)?.Value ?? PurchaseValue;


    public ICollection<TokenHistoryEntity> History { get; set; }
}