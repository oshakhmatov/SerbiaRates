using SerbiaRates.Models;

namespace SerbiaRates.Data.Repos;

public interface IRepo
{
    public Task Add<T>(T entity, CancellationToken token = default) where T : class;

    public Task<AverageRate?> GetLastAverageRate(CancellationToken token = default);
    public Task<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token = default);

    public Task<Company[]> GetCompanies(CancellationToken token = default);
    public Task<AverageRate[]> GetAverageRates(int take = 10, CancellationToken token = default);
    public Task<ExchangeRate[]> GetExchangeRates(CancellationToken token = default);
}
