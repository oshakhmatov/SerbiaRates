using SerbiaRates.Domain;

namespace SerbiaRates.Modules.ExchangeRates.RatesUpdater;

public sealed class DailyRateCoupleDto
{
    public required DateOnly Date { get; init; }
    public required RateDto Euro { get; init; }
    public required RateDto Dollar { get; init; }

    public DailyRateCouple AsExchangeRates(int companyId) => new()
    {
        CompanyId = companyId,
        Date = Date,
        Rates = new List<ExchangeRate>()
        {
            new ExchangeRate()
            {
                Buy = Euro.Buy,
                Sell = Euro.Sell,
                CurrencyId = Const.EuroId
            },
            new ExchangeRate()
            {
                Buy = Dollar.Buy,
                Sell = Dollar.Sell,
                CurrencyId = Const.DollarId
            }
        }
    };

    public DailyAverageRateCouple? AsAverageRates() =>
        Euro.Average.HasValue && Dollar.Average.HasValue
        ? new DailyAverageRateCouple()
        {
			Date = Date,
			Rates = new List<AverageRate>()
			{
				new AverageRate()
				{
					CurrencyId = Const.EuroId,
					Price = Euro.Average.Value
				},
				new AverageRate()
				{
					CurrencyId = Const.DollarId,
					Price = Dollar.Average.Value
				}
			}
		} 
        : null;
}

public sealed class RateDto
{
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }
	public decimal? Average { get; init; }
}
