using System.Text.Json.Serialization;

namespace SerbiaRates.ViewModels;

public sealed class ChartsViewModel
{
    public required Point[] Points { get; init; }
}

public sealed class Point
{
    [JsonPropertyName("USD")]
    public required decimal USD { get; init; }
    [JsonPropertyName("EUR")]
    public required decimal EUR { get; init; }
    public required string Date { get; init; }
}
