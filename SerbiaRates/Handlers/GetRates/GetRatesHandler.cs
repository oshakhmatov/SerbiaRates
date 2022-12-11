using SerbiaRates.Data.Repos.Abstractions;

namespace SerbiaRates.Handlers.GetRates;

public class GetRatesHandler
{
	private readonly IRatesRepo ratesRepo;

	public GetRatesHandler(IRatesRepo ratesRepo)
	{
		this.ratesRepo = ratesRepo;
	}

	public async Task<RatesViewModel?> Handle()
	{
		var averageRate = await ratesRepo.GetLastAverageRate();
		if (averageRate is null)
			return null;

		var exchangeRates = await ratesRepo.GetExchangeRates();

		return new RatesViewModel()
		{
			Date = averageRate.Date,
			AverageEuro = averageRate.Euro,
			AverageDollar = averageRate.Dollar,
			Rates = exchangeRates.Select(er => new CompanyRatesDto()
			{
				CompanyName = er.Company!.Name,
				Date = er.Date,
				Euro = new RateDto()
				{
					Sell = er.EuroSell,
					Buy = er.EuroBuy
				},
				Dollar = new RateDto()
				{
					Sell = er.DollarSell,
					Buy = er.DollarBuy
				}
			})
			.ToArray()
		};
	}
}
