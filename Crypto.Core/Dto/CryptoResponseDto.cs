using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="Id"> Request Identifier </param>
/// <param name="Method"> The method to be invoked </param>
/// <param name="Code"> Response code </param>
/// <param name="Result"> Result </param>
/// <param name="InstrumentName"> Instrument Name </param>
public record CryptoResponseDto<T>([property: JsonPropertyName("id")] int Id, [property: JsonPropertyName("method")] string Method, [property: JsonPropertyName("code")] int Code, [property: JsonPropertyName("result")] DataResponseDto<T> Result, [property: JsonPropertyName("instrument_name")] string? InstrumentName);
