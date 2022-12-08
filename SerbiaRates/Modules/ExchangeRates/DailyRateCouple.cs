namespace SerbiaRates.Modules.ExchangeRates;

public sealed class DailyRateCouple
{
    public int Id { get; init; }
    public required DateOnly Date { get; init; }

    public List<ExchangeRate>? Rates { get; init; }

    public required int CompanyId { get; init; }
    public Company? Company { get; init; }
}
