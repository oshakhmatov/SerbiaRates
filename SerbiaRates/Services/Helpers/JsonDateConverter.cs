using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerbiaRates.Services.Helpers;

public sealed class JsonDateConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateOnly.ParseExact(reader.GetString()!,
            Const.DateFormat, CultureInfo.InvariantCulture);

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(
            Const.DateFormat, CultureInfo.InvariantCulture));
}
