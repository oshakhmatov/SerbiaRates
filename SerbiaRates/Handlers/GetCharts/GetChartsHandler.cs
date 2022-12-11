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
			Points = averageRates.Select(ar => new Point()
			{
				Date = ar.Date.ToString("MMMM dd"),
				EUR = ar.Euro,
				USD = ar.Dollar
			})
			.ToArray()
		};
	}
}
