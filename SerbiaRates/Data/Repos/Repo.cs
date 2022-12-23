using Microsoft.EntityFrameworkCore;
using SerbiaRates.Models;

namespace SerbiaRates.Data.Repos;

public sealed class Repo : IRepo
{
    private readonly AppDbContext dbContext;

    public Repo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Add<T>(T entity, CancellationToken token = default) where T : class
    {
        await dbContext.AddAsync(entity, token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task<Company[]> GetCompanies(CancellationToken token = default)
    {
        return await dbContext.Companies.ToArrayAsync(token);
    }

    public async Task<AverageRate?> GetLastAverageRate(CancellationToken token = default)
    {
        return await dbContext.AverageRates
            .OrderByDescending(ar => ar.Date)
            .FirstOrDefaultAsync(token);
    }

    public async Task<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token = default)
    {
        return await dbContext.ExchangeRates
            .Where(er => er.CompanyId == companyId)
            .OrderByDescending(er => er.Date)
            .FirstOrDefaultAsync(token);
    }

    public async Task<ExchangeRate[]> GetExchangeRates(CancellationToken token = default)
    {
        return await dbContext.ExchangeRates
            .Include(er => er.Company)
            .OrderByDescending(er => er.Date)
            .GroupBy(c => c.Company!.Name)
            .Select(g => g.First())
            .ToArrayAsync(token);
    }

    public async Task<AverageRate[]> GetAverageRates(int take = 30, CancellationToken token = default)
    {
        return await dbContext.AverageRates
            .OrderByDescending(ar => ar.Date)
            .Take(take)
            .ToArrayAsync(token);
    }
}
