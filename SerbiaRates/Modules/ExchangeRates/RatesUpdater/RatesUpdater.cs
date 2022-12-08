using SerbiaRates.Domain;
using SerbiaRates.Modules.ExchangeRates.RatesUpdater.RateParsers;
using SerbiaRates.Modules.Shared;

namespace SerbiaRates.Modules.ExchangeRates.RatesUpdater;

public class RatesUpdater : HostedServiceBase
{
    private static readonly HttpClient httpClient = new();

    private readonly IServiceProvider serviceProvider;

    public RatesUpdater(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected override TimeSpan Interval => TimeSpan.FromHours(1);

    protected override async ValueTask DoWork(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<IExchangeRateRepo>();

        foreach (var company in await repo.GetCompanies(stoppingToken))
        {
            var latestRateCouple = await repo.GetLatestRateCouple(company.Id, stoppingToken);

            if (latestRateCouple is not null &&
				latestRateCouple.Date == DateOnly.FromDateTime(DateTime.Today))
                continue;

            var result = await httpClient.GetStringAsync(company.Url, stoppingToken);

            var parser = CreateParser(company.Id);

            var dailyRateCoupleDto = parser.Parse(result);

            if (latestRateCouple is not null && latestRateCouple.Date == dailyRateCoupleDto.Date)
                continue;

            var exchangeRates = dailyRateCoupleDto.AsExchangeRates(company.Id);
            var averageRates = dailyRateCoupleDto.AsAverageRates();

			await repo.SaveRates(exchangeRates, averageRates, stoppingToken);
		}
    }

    private static IRatesParser CreateParser(int providerId) => providerId switch
    {
        Const.GagaId => new GagaRateParser(),
        Const.PostanskaId => new PostanskaRateParser(),
        _ => throw new NotImplementedException($"No parser for provider with ID {providerId} is registred")
    };
}
