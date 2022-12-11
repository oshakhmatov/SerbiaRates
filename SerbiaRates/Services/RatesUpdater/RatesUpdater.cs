using SerbiaRates.Data.Repos.Abstractions;
using SerbiaRates.Services.RatesUpdater.RateParsers;

namespace SerbiaRates.Services.RatesUpdater;

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

        var repo = scope.ServiceProvider.GetRequiredService<IRatesRepo>();

        foreach (var company in await repo.GetCompanies(stoppingToken))
        {
            var lastExchangeRate = await repo.GetLastExchangeRate(company.Id, stoppingToken);

            if (lastExchangeRate is not null &&
				lastExchangeRate.CreateDate == DateOnly.FromDateTime(DateTime.Today))
                continue;

            var url = BuildUrl(company.Id, company.Url);

            var result = await httpClient.GetStringAsync(url, stoppingToken);

            var parser = CreateParser(company.Id);

            var dailyRateCoupleDto = parser.Parse(result);

            if (lastExchangeRate is not null && lastExchangeRate.Date == dailyRateCoupleDto.Date)
                continue;

            var exchangeRate = dailyRateCoupleDto.AsExchangeRate(company.Id);
            var averageRate = dailyRateCoupleDto.AsAverageRate();

            await repo.Add(exchangeRate, stoppingToken);

            if (averageRate is not null)
                await repo.Add(averageRate, stoppingToken);
		}
    }

    private static string BuildUrl(int providerId, string url) => providerId switch
    {
        Const.GagaId => url,
        Const.PostanskaId => url,
        Const.EldoradoId => url,
        Const.TackaId => url,
        Const.RaiffId => url + DateTime.Today.ToString("dd.MM.yyyy"),
        _ => throw new NotImplementedException($"No parser for provider with ID {providerId} is registred")
    };

	private static IRatesParser CreateParser(int providerId) => providerId switch
    {
        Const.GagaId => new GagaRateParser(),
        Const.PostanskaId => new PostanskaRateParser(),
		Const.EldoradoId => new EldoradoParser(),
        Const.TackaId => new TackaParser(),
		Const.RaiffId => new RaiffParser(),
		_ => throw new NotImplementedException($"No url builder for provider with ID {providerId} is registred")
    };
}
