using SerbiaRates.Data.Repos;
using SerbiaRates.Models;
using SerbiaRates.Services.Helpers;
using SerbiaRates.Services.HttpService;
using SerbiaRates.Services.ParserCreator;
using SerbiaRates.Services.RateBuilder;

namespace SerbiaRates.Services.RatesUpdater;

public sealed class RatesUpdater : IRatesUpdater
{
    private readonly IRepo repo;
    private readonly IWebProvider webProvider;
    private readonly IParserCreator parserCreator;
    private readonly IRateBuilder rateBuilder;

    public RatesUpdater(
        IRepo repo,
        IWebProvider webProvider,
        IParserCreator parserCreator,
        IRateBuilder rateBuilder)
    {
        this.repo = repo;
        this.webProvider = webProvider;
        this.parserCreator = parserCreator;
        this.rateBuilder = rateBuilder;
    }

    public async Task UpdateRates(CancellationToken token)
    {
        foreach (var company in await repo.GetCompanies(token))
        {
            try
            {
                var lastRate = await repo.GetLastExchangeRate(company.Id, token);

                if (RateIsUpToDate(lastRate))
                    continue;

                var result = await webProvider.Request(company.Url, token);

                var parser = parserCreator.CreateParser(company.Id);

                var ratesCouple = parser.Parse(result);

                if (RateIsAlreadyExists(lastRate, ratesCouple))
                    continue;

                var exchangeRate = rateBuilder.BuildExchangeRate(ratesCouple, company.Id);
                var averageRate = rateBuilder.BuildAverageRate(ratesCouple);

                await repo.UpdateRates(exchangeRate, averageRate, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private static bool RateIsUpToDate(ExchangeRate? exchangeRate)
    {
        return exchangeRate is not null && exchangeRate.CreateDate == Date.Today();
    }

    private static bool RateIsAlreadyExists(ExchangeRate? exchangeRate, RatesCoupleDto ratesCouple)
    {
        return exchangeRate is not null && exchangeRate.Date == ratesCouple.Date;
    }
}
