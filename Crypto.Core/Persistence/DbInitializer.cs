using Crypto.Core.Entities;

namespace Crypto.Core.Persistence;

public class DbInitializer(DataContext dataContext)
{
    public void Seed()
    {
        if (dataContext.Tokens.Any())
            return;

        dataContext.Tokens.Add(
            new TokenEntity
            {
                TokenCode = "CKBUSD-INDEX",
                ExchangeHistory = { new TokenExchangeHistoryEntity
                    {
                        RecordedDate = new DateTime(2024,04,13,21,50,00),
                        ExchangeType = ExchangeTypeEnum.Buy,
                        Value = 0.02401M
                    } }
            }
        );

        dataContext.SaveChanges();
    }
}