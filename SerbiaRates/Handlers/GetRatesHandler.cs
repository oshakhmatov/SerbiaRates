using SerbiaRates.Data.Repos;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Handlers;

public sealed class GetRatesHandler
{
    private readonly IRepo ratesRepo;

    public GetRatesHandler(IRepo ratesRepo)
    {
        this.ratesRepo = ratesRepo;
    }

    public async Task<RatesViewModel?> Handle(CancellationToken token)
    {
        var averageRate = await ratesRepo.GetLastAverageRate(token);
        if (averageRate is null)
            return null;

        var exchangeRates = await ratesRepo.GetExchangeRates(token);

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
