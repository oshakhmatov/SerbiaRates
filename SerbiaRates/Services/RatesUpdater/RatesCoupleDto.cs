using SerbiaRates.Models;

namespace SerbiaRates.Services.RatesUpdater;

public sealed class RatesCoupleDto
{
    public required DateOnly Date { get; init; }
    public required RateDto Euro { get; init; }
    public required RateDto Dollar { get; init; }

    public ExchangeRate AsExchangeRate(int companyId) => new()
    {
        CompanyId = companyId,
        Date = Date,
        EuroBuy = Euro.Buy,
        EuroSell = Euro.Sell,
        DollarBuy = Dollar.Buy,
        DollarSell = Dollar.Sell
    };

    public AverageRate? AsAverageRate() =>
        Euro.Average.HasValue && Dollar.Average.HasValue
        ? new AverageRate()
        {
            Date = Date,
            Euro = Euro.Average.Value,
            Dollar = Dollar.Average.Value
        }
        : null;
}

public sealed class RateDto
{
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }
    public decimal? Average { get; init; }
}
