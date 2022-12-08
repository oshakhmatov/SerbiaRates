using SerbiaRates.Modules.Shared;

namespace SerbiaRates.Modules.ExchangeRates;

public sealed class AverageRate
{
    public int Id { get; init; }
    public required decimal Price { get; init; }

    public required int CurrencyId { get; init; }
    public Currency? Currency { get; init; }
    public int DailyAverageRateCoupleId { get; init; }
    public DailyAverageRateCouple? DailyAverageRateCouple { get; init; }
}
