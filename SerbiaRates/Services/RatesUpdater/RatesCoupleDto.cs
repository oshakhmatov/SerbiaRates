namespace SerbiaRates.Services.RatesUpdater;

public sealed class RatesCoupleDto
{
    public required DateOnly Date { get; init; }
    public required RateDto Euro { get; init; }
    public required RateDto Dollar { get; init; }
}

public sealed class RateDto
{
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }
    public decimal? Average { get; init; }
}
