using Crypto.Core.Helpers;
using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="Value"> Value </param>
/// <param name="Timestamp"> Timestamp </param>
public record ValuationsResponseDto([property: JsonPropertyName("v")] decimal Value, [property: JsonPropertyName("t"), JsonConverter(typeof(UnixMilliDateTimeConverter))] DateTime Timestamp);
