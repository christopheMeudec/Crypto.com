namespace Crypto.Core.Entities;

public class TokenExchangeHistoryEntity
{
    public int Id { get; set; }
    public DateTime RecordedDate { get; set; }
    public ExchangeTypeEnum ExchangeType { get; set; }
    public decimal Value { get; set; }

    public string? TokenCode { get; set; }
    public TokenEntity? Token { get; set; }
}

public enum ExchangeTypeEnum
{
    Sell = 0,
    Buy = 1
}