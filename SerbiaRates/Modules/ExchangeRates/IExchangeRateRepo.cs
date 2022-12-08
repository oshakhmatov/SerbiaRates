namespace SerbiaRates.Modules.ExchangeRates;

public interface IExchangeRateRepo
{
    public ValueTask<Company[]> GetCompanies(CancellationToken token = default);

    public Task<RateListViewModel> GetRates(CancellationToken token = default);
    public ValueTask<DailyRateCouple?> GetLatestRateCouple(int companyId, CancellationToken token = default);
    public ValueTask SaveRates(DailyRateCouple exchangeRates, DailyAverageRateCouple averageRates, CancellationToken token = default);
}
