namespace Crypto.Core.Entities;

public class TokenValueHistoryEntity
{
    public int Id { get; set; }
    public  DateTime RecordedDate { get; set; }
    public decimal Value{ get; set; }

    public string? TokenCode { get; set; }
    public TokenEntity? Token { get; set; }
}