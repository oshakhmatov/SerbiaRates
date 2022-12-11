using System.Text.Json.Serialization;

namespace SerbiaRates.Handlers.GetCharts;

public sealed record ChartsViewModel
{
	public required Point[] Points { get; init; }
}

public sealed record Point
{
	[JsonPropertyName("USD")]
	public required decimal USD { get; init; }
	[JsonPropertyName("EUR")]
	public required decimal EUR { get; init; }
	public required string Date { get; init; }
}
