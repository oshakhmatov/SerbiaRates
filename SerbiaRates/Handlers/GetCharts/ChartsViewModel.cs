namespace SerbiaRates.Handlers.GetCharts;

public sealed record ChartsViewModel
{
	public required Point[] EuroPoints { get; init; }
	public required Point[] DollarPoints { get; init; }
}

public sealed record Point
{
	public required decimal Rate { get; init; }
	public required DateOnly Date { get; init; }
}
