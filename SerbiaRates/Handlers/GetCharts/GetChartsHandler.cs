using SerbiaRates.Data.Repos.Abstractions;

namespace SerbiaRates.Handlers.GetCharts;

public class GetChartsHandler
{
	private readonly IRatesRepo ratesRepo;

	public GetChartsHandler(IRatesRepo ratesRepo)
	{
		this.ratesRepo = ratesRepo;
	}

	public async ValueTask<ChartsViewModel?> Handle()
	{
		var averageRates = await ratesRepo.GetAverageRates();
		if (averageRates is null)
			return null;

		return new ChartsViewModel()
		{
			EuroPoints = averageRates
				.Select(r => new Point()
				{
					Date = r.Date,
					Rate = r.Euro
				})
				.ToArray(),
			DollarPoints = averageRates
				.Select(r => new Point()
				{
					Date = r.Date,
					Rate = r.Dollar
				})
				.ToArray()
		};
	}
}
