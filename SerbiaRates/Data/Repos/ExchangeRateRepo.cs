using Microsoft.EntityFrameworkCore;
using SerbiaRates.Domain;
using SerbiaRates.Modules.ExchangeRates;

namespace SerbiaRates.Data.Repos;

public sealed class ExchangeRateRepo : IExchangeRateRepo
{
    private readonly AppDbContext dbContext;

    public ExchangeRateRepo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<RateListViewModel> GetRates(CancellationToken token = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var dailyAverageRateCouple = await dbContext.DailyAverageRateCouples
            .Include(darc => darc.Rates)
            .Where(darc => darc.Date == today)
            .FirstOrDefaultAsync(token);

        var averageEuro = dailyAverageRateCouple.Rates.First(r => r.CurrencyId == Const.EuroId).Price;
        var averageDollar = dailyAverageRateCouple.Rates.First(r => r.CurrencyId == Const.DollarId).Price;

		var rates = await dbContext.DailyRateCouples
			.Include(drc => drc.Company)
			.Include(drc => drc.Rates)
			.GroupBy(er => er.Company!.Name)
			.Select(g => new CompanyRatesDto()
			{
				CompanyName = g.Key,
				Date = g.Max(r => r.Date),
				Euro = g
					.OrderByDescending(drc => drc.Date)
					.SelectMany(drc => drc.Rates!)
					.Where(r => r.CurrencyId == Const.EuroId)
					.Select(r => new RateDto()
					{
						Buy = r.Buy,
						Sell = r.Sell,
						BuyDelta = CalcDelta(r.Buy, averageEuro),
						SellDelta = CalcDelta(r.Sell, averageEuro),
					})
					.First(),
				Dollar = g
					.OrderByDescending(drc => drc.Date)
					.SelectMany(drc => drc.Rates!)
					.Where(r => r.CurrencyId == Const.DollarId)
					.Select(r => new RateDto()
					{
						Buy = r.Buy,
						Sell = r.Sell,
						BuyDelta = CalcDelta(r.Buy, averageDollar),
						SellDelta = CalcDelta(r.Sell, averageDollar),
					})
					.First()
			})
			.ToArrayAsync(token);

		return new RateListViewModel()
        {
            Rates = rates,
            Date = dailyAverageRateCouple.Date,
            AverageEuro = dailyAverageRateCouple.Rates.First(r => r.CurrencyId == Const.EuroId).Price,
            AverageDollar = dailyAverageRateCouple.Rates.First(r => r.CurrencyId == Const.DollarId).Price
        };
    }

    public async ValueTask<DailyRateCouple?> GetLatestRateCouple(int companyId, CancellationToken token = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        return await dbContext.DailyRateCouples
            .Where(er => er.CompanyId == companyId)
            .OrderByDescending(er => er.Date)
            .FirstOrDefaultAsync(token);
    }

    public async ValueTask SaveRates(DailyRateCouple exchangeRates, DailyAverageRateCouple? averageRates = null, CancellationToken token = default)
    {
        await dbContext.AddAsync(exchangeRates, token);

        if (averageRates is not null)
			await dbContext.AddAsync(averageRates, token);

		await dbContext.SaveChangesAsync(token);
	}

	public async ValueTask<Company[]> GetCompanies(CancellationToken stoppingToken = default)
	{
		return await dbContext.Companies.ToArrayAsync(stoppingToken);
	}

	private static decimal CalcDelta(decimal companyRate, decimal averageRate)
	{
		return Math.Round((companyRate - averageRate) / averageRate * 100, 2);
	}
}
