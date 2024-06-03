using System.Net.Http.Json;
using Crypto.Core.Dto;
using Crypto.Core.Settings;

namespace Crypto.Core.Services;

//https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#introduction
public class CryptoService : ICryptoService
{
    private readonly HttpClient _httpClient;
    private readonly CryptoSettings _cryptoSettings;

    public CryptoService(CryptoSettings cryptoSettings)
    {
        _cryptoSettings = cryptoSettings;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(cryptoSettings.Url);
    }

    public async Task<List<InstrumentsResponseDto>> GetInstruments(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<CryptoResponseDto<List<InstrumentsResponseDto>>>(CryptoConstants.InstrumentsUrl, cancellationToken: cancellationToken);

        return result.Result.Data;
    }

    public async Task<List<TickersResponseDto>> GetTickers(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<CryptoResponseDto<List<TickersResponseDto>>>(CryptoConstants.TickersUrl, cancellationToken: cancellationToken);

        return result.Result.Data;
    }

    public async Task<TickersResponseDto> GetTicker(string instrumentName, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<CryptoResponseDto<List<TickersResponseDto>>>(CryptoConstants.TickersUrl + $"?instrument_name={instrumentName}", cancellationToken: cancellationToken);

        return result.Result.Data.First();
    }

    public async Task<List<ValuationsResponseDto>> GeValuations(string instrumentName, int points = 100, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<CryptoResponseDto<List<ValuationsResponseDto>>>(CryptoConstants.ValuationsUrl + $"?instrument_name={instrumentName}&valuation_type=index_price&count={points}", cancellationToken: cancellationToken);

        return result.Result.Data;
    }
    private static string Sign(string time)
    {
        return string.Empty;
    }
}

public interface ICryptoService
{
    Task<List<InstrumentsResponseDto>> GetInstruments(CancellationToken cancellationToken = default);
    Task<TickersResponseDto> GetTicker(string instrumentName, CancellationToken cancellationToken = default);
    Task<List<TickersResponseDto>> GetTickers(CancellationToken cancellationToken = default);
    Task<List<ValuationsResponseDto>> GeValuations(string instrumentName, int points = 100, CancellationToken cancellationToken = default);
}

internal class CryptoConstants
{
    public const string InstrumentsUrl = "/exchange/v1/public/get-instruments";
    public const string BookUrl = "/exchange/v1/public/get-book";
    public const string CandleStickUrl = "/exchange/v1/public/get-candlestick";
    public const string TradesUrl = "/exchange/v1/public/get-trades";
    public const string TickersUrl = "/exchange/v1/public/get-tickers";
    public const string ValuationsUrl = "/exchange/v1/public/get-valuations";
    public const string ExpiredSettlementPriceUrl = "/exchange/v1/public/get-expired-settlement-price";
    public const string InsuranceUrl = "/exchange/v1/public/get-insurance";
}