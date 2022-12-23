using SerbiaRates.Data.Repos;
using SerbiaRates.Services.RatesUpdater.RateParsers;

namespace SerbiaRates.Services.RatesUpdater;

public sealed class RatesUpdater : HostedServiceBase
{
	private static readonly HttpClient httpClient = new();

	private readonly IServiceProvider serviceProvider;

	public RatesUpdater(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	protected override TimeSpan Interval => TimeSpan.FromHours(Const.RatesUpdaterIntervalHours);

	protected override async Task DoWork(CancellationToken stoppingToken)
	{
		using var scope = serviceProvider.CreateScope();

		var repo = scope.ServiceProvider.GetRequiredService<IRepo>();

		foreach (var company in await repo.GetCompanies(stoppingToken))
		{
			try
			{
				var lastExchangeRate = await repo.GetLastExchangeRate(company.Id, stoppingToken);

				if (lastExchangeRate is not null &&
					lastExchangeRate.CreateDate == DateOnly.FromDateTime(DateTime.Today))
					continue;

				var result = await httpClient.GetStringAsync(company.Url, stoppingToken);

				var parser = CreateParser(company.Id);

				var exchangeRateDto = parser.Parse(result);

				if (lastExchangeRate is not null && lastExchangeRate.Date == exchangeRateDto.Date)
					continue;

				var exchangeRate = exchangeRateDto.AsExchangeRate(company.Id);
				var averageRate = exchangeRateDto.AsAverageRate();

				await repo.Add(exchangeRate, stoppingToken);

				if (averageRate is not null)
					await repo.Add(averageRate, stoppingToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}

	private static IRatesParser CreateParser(int providerId) => providerId switch
	{
		Const.GagaId => new GagaRateParser(),
		Const.PostanskaId => new PostanskaRateParser(),
		Const.EldoradoId => new EldoradoParser(),
		Const.TackaId => new TackaParser(),
		_ => throw new NotImplementedException($"No parser for provider with ID {providerId} is registred")
	};
}
