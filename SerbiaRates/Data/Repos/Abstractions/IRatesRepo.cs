using SerbiaRates.Models;

namespace SerbiaRates.Data.Repos.Abstractions;

public interface IRatesRepo
{
	public ValueTask Add<T>(T entity, CancellationToken token = default) where T : class;

	public ValueTask<Company[]> GetCompanies(CancellationToken token = default);

	public ValueTask<AverageRate?> GetLastAverageRate(CancellationToken token = default);
	public ValueTask<ExchangeRate?> GetLastExchangeRate(int companyId, CancellationToken token = default);

	public ValueTask<AverageRate[]> GetAverageRates(int take = 10, CancellationToken token = default);
	public ValueTask<ExchangeRate[]> GetExchangeRates(DateOnly date, CancellationToken token = default);	
}
