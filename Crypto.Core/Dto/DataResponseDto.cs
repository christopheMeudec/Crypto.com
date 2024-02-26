using System.Text.Json.Serialization;

namespace Crypto.Core.Dto;

/// <param name="Data"> Data </param>
public record DataResponseDto<T>([property: JsonPropertyName("data")] T Data);
