using SerbiaRates.Modules.Shared;

namespace SerbiaRates.Modules.ExchangeRates;

public sealed class ExchangeRate
{
    public int Id { get; init; }
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }

    public int DailyRateCoupleId { get; init; }
    public DailyRateCouple? DailyRateCouple { get; init; }

    public required int CurrencyId { get; init; }
    public Currency? Currency { get; init; }
}
