using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data.Repos.Abstractions;
using SerbiaRates.Models;

namespace SerbiaRates.Data.Repos;

public sealed class RatesRepo : IRatesRepo
{
    private readonly AppDbContext dbContext;

    public RatesRepo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

	public async ValueTask Add<T>(T entity, CancellationToken token = default) where T : class
	{
		await dbContext.AddAsync(entity, token);
		await dbContext.SaveChangesAsync(token);
	}

	public async ValueTask<Company[]> GetCompanies(CancellationToken token = default)
	{
		return await dbContext.Companies.ToArrayAsync(token);
	}

	public async ValueTask<AverageRate?> GetLastAverageRate(CancellationToken token = default)
    {
		return await dbContext.AverageRates
            .OrderByDescending(ar => ar.Date)
			.FirstOrDefaultAsync(token);
	}

	public async ValueTask<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token = default)
	{
		return await dbContext.ExchangeRates
			.Where(er => er.CompanyId == companyId)
			.OrderByDescending(er => er.Date)
			.FirstOrDefaultAsync(token);
	}

	public async ValueTask<ExchangeRate[]> GetExchangeRates(CancellationToken token = default)
	{
		return await dbContext.ExchangeRates
			.Include(er => er.Company)
			.OrderByDescending(er => er.Date)
			.GroupBy(c => c.Company!.Name)
			.Select(g => g.First())
			.ToArrayAsync(token);
	}

    public async ValueTask<AverageRate[]> GetAverageRates(int take = 10, CancellationToken token = default)
    {
        return await dbContext.AverageRates
            .Take(take)
            .ToArrayAsync(token);
    }
}
