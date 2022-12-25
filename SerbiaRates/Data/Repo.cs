using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data.Abstractions;
using SerbiaRates.Models;

namespace SerbiaRates.Data;

public sealed class Repo : IRepo
{
    private readonly AppDbContext dbContext;

    public Repo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task UpdateRates(ExchangeRate exchangeRate, AverageRate? averageRate, CancellationToken token)
    {
        dbContext.Add(exchangeRate);

        if (averageRate is not null)
            dbContext.Add(averageRate);

        await dbContext.SaveChangesAsync(token);
    }

    public async Task<Company[]> GetCompanies(CancellationToken token)
    {
        return await dbContext.Companies.ToArrayAsync(token);
    }

    public async Task<AverageRate?> GetLastAverageRate(CancellationToken token)
    {
        return await dbContext.AverageRates
            .OrderByDescending(ar => ar.Date)
            .FirstOrDefaultAsync(token);
    }

    public async Task<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token)
    {
        return await dbContext.ExchangeRates
            .Where(er => er.CompanyId == companyId)
            .OrderByDescending(er => er.Date)
            .FirstOrDefaultAsync(token);
    }

    public async Task<ExchangeRate[]> GetExchangeRates(CancellationToken token)
    {
        return await dbContext.ExchangeRates
            .Include(er => er.Company)
            .GroupBy(c => c.Company!.Name)
            .Select(g => g.OrderByDescending(er => er.Date).First())
            .ToArrayAsync(token);
    }

    public async Task<AverageRate[]> GetAverageRates(int take, CancellationToken token)
    {
        return await dbContext.AverageRates
            .OrderByDescending(ar => ar.Date)
            .Take(take)
            .Reverse()
            .ToArrayAsync(token);
    }
}
