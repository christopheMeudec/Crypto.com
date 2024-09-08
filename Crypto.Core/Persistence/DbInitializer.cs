using Crypto.Core.Entities;

namespace Crypto.Core.Persistence;

public class DbInitializer(DataContext dataContext)
{
    public void Seed()
    {
        if (dataContext.Tokens.Any())
            return;

        var tokenEntities = new List<TokenEntity>
        {
            new () 
            {
                TokenCode = "ENSUSD-INDEX",
                ExchangeHistory = { new TokenExchangeHistoryEntity
                {
                    RecordedDate = new DateTime(2024,06,24,17,23,03),
                    ExchangeType = ExchangeTypeEnum.Buy,
                    Value = 2309.838033M
                } }
            },
            // new () 
            // {
            //     TokenCode = "ETHUSD-INDEX",
            //     ExchangeHistory = { new TokenExchangeHistoryEntity
            //     {
            //         RecordedDate = new DateTime(2022,01,24,17,23,03),
            //         ExchangeType = ExchangeTypeEnum.Buy,
            //         Value = 2309.838033M
            //     } }
            // },
            // new() 
            // {
            //     TokenCode = "BTCUSD-INDEX",
            //     ExchangeHistory = { new TokenExchangeHistoryEntity
            //     {
            //         RecordedDate = new DateTime(2024,03,25,17,58,00),
            //         ExchangeType = ExchangeTypeEnum.Sell,
            //         Value = 77240.99M
            //     } }
            // },
            // new()
            // {
            //     TokenCode = "CKBUSD-INDEX",
            //     ExchangeHistory = { new TokenExchangeHistoryEntity
            //     {
            //         RecordedDate = new DateTime(2024,04,13,21,50,00),
            //         ExchangeType = ExchangeTypeEnum.Buy,
            //         Value = 0.02401M
            //     } }
            // }
        };
        
        dataContext.Tokens.AddRange(tokenEntities);

        dataContext.SaveChanges();
    }
}