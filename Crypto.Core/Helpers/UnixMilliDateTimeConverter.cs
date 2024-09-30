using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crypto.Core.Helpers;

internal class UnixMilliDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()).LocalDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(((DateTimeOffset)value).ToUnixTimeMilliseconds());
    }
}
