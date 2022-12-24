using SerbiaRates.Models;

namespace SerbiaRates.Data.Repos;

public interface IRepo
{
    public Task UpdateRates(ExchangeRate exchangeRate, AverageRate? averageRate, CancellationToken token);

    public Task<AverageRate?> GetLastAverageRate(CancellationToken token);
    public Task<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token);

    public Task<Company[]> GetCompanies(CancellationToken token);
    public Task<AverageRate[]> GetAverageRates(int take, CancellationToken token);
    public Task<ExchangeRate[]> GetExchangeRates(CancellationToken token);
}
