using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="Symbol"> e.g. BTCUSD-PERP </param>
/// <param name="InstrumentType"> e.g. PERPETUAL_SWAP </param>
/// <param name="DisplayName"> e.g. BTCUSD Perpetual </param>
/// <param name="BaseCurrency"> Base currency, e.g. BTC </param>
/// <param name="QuoteCurrency"> Quote currency, e.g. USD </param>
/// <param name="QuoteDecimals"> Minimum decimal place for price field </param>
/// <param name="QuantityDecimals"> Minimum decimal place for qty field </param>
/// <param name="PriceTickSize"> Minimum price tick size </param>
/// <param name="QuantityTickSize"> Minimum trading quantity / tick size </param>
/// <param name="MaxLeverage"> Max leverage of the product </param>
/// <param name="Tradable"> True or false </param>
/// <param name="ExpiryTimestampInMillisecond"> Expiry timestamp in millisecond </param>
/// <param name="UnderlyingSymbol"> Underlying symbol </param>
public record InstrumentsResponseDto([property: JsonPropertyName("symbol")] string Symbol, [property: JsonPropertyName("inst_type")] string InstrumentType, [property: JsonPropertyName("display_name")] string DisplayName, [property: JsonPropertyName("base_ccy")] string BaseCurrency, [property: JsonPropertyName("quote_ccy")] string QuoteCurrency, [property: JsonPropertyName("quote_decimals")] int QuoteDecimals, [property: JsonPropertyName("quantity_decimals")] int QuantityDecimals, [property: JsonPropertyName("price_tick_size")] string PriceTickSize, [property: JsonPropertyName("qty_tick_size")] string QuantityTickSize, [property: JsonPropertyName("max_leverage")] string MaxLeverage, [property: JsonPropertyName("tradable")] bool Tradable, [property: JsonPropertyName("expiry_timestamp_ms")] long ExpiryTimestampInMillisecond, [property: JsonPropertyName("underlying_symbol")] string UnderlyingSymbol);
