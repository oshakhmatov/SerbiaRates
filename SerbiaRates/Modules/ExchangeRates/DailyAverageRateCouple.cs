namespace SerbiaRates.Modules.ExchangeRates;

public class DailyAverageRateCouple
{
	public int Id { get; init; }
	public required DateOnly Date { get; init; }

	public List<AverageRate>? Rates { get; init; }
}
