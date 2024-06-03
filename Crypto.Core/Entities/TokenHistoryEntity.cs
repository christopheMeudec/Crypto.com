namespace Crypto.Core.Entities;

public class TokenHistoryEntity
{
    public int Id { get; set; }
    public required string TokenCode { get; set; }
    public  DateTime RecordedDate { get; set; }
    public decimal Value{ get; set; }

    public TokenEntity Token { get; set; }
}