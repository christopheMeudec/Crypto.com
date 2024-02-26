using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="HighestTrade"> Price of the 24h highest trade </param>
/// <param name="LowestTrade"> Price of the 24h lowest trade, null if there weren't any trades </param>
/// <param name="LatestTrade"> The price of the latest trade, null if there weren't any trades </param>
/// <param name="InstrumentName"> Instrument name </param>
/// <param name="TradedVolume"> The total 24h traded volume </param>
/// <param name="TradedVolumeValueInUsd"> The total 24h traded volume value (in USD) </param>
/// <param name="OpenInterest"> Open interest </param>
/// <param name="PriceChange"> 24-hour price change, null if there weren't any trades </param>
/// <param name="BestBidPrice"> The current best bid price, null if there aren't any bids </param>
/// <param name="BestAskPrice"> The current best ask price, null if there aren't any asks </param>
/// <param name="Timestamp"> Timestamp </param>
public record TickersResponseDto([property: JsonPropertyName("h")] string HighestTrade, [property: JsonPropertyName("l")] string? LowestTrade, [property: JsonPropertyName("a")] string? LatestTrade, [property: JsonPropertyName("i")] string InstrumentName, [property: JsonPropertyName("v")] string TradedVolume, [property: JsonPropertyName("vv")] string TradedVolumeValueInUsd, [property: JsonPropertyName("oi")] string OpenInterest, [property: JsonPropertyName("c")] string? PriceChange, [property: JsonPropertyName("b")] string? BestBidPrice, [property: JsonPropertyName("k")] string? BestAskPrice, [property: JsonPropertyName("t")] long Timestamp);
