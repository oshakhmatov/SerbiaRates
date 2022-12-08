namespace SerbiaRates.Modules.ExchangeRates;

public class Company
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }

    public List<DailyRateCouple>? DailyRateCouples { get; init; }
}
