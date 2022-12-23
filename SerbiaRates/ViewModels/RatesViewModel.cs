namespace SerbiaRates.ViewModels;

public sealed class RatesViewModel
{
    public required DateOnly Date { get; init; }
    public required decimal AverageEuro { get; init; }
    public required decimal AverageDollar { get; init; }
    public required CompanyRatesDto[] Rates { get; init; }
}

public sealed class CompanyRatesDto
{
    public required string CompanyName { get; init; }
    public required DateOnly Date { get; init; }
    public required RateDto Euro { get; init; }
    public required RateDto Dollar { get; init; }
}

public sealed class RateDto
{
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }
}
