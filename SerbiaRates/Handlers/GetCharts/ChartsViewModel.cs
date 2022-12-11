namespace SerbiaRates.Handlers.GetCharts;

public sealed record ChartsViewModel
{
	public required Point[] Points { get; init; }
}

public sealed record Point
{
	public required decimal USD { get; init; }
	public required decimal EUR { get; init; }
	public required string Date { get; init; }
}
