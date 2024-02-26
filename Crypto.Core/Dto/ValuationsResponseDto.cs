using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="Value"> Value </param>
/// <param name="Timestamp"> Timestamp </param>
public record ValuationsResponseDto([property: JsonPropertyName("v")] string Value, [property: JsonPropertyName("t")] long Timestamp);
