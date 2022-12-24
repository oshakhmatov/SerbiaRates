using SerbiaRates.Models;
using SerbiaRates.Services.RatesUpdater;

namespace SerbiaRates.Services.RatesUpdater.RateBuilder;

public sealed class RateBuilder : IRateBuilder
{
    public ExchangeRate BuildExchangeRate(RatesCoupleDto ratesCouple, int companyId)
    {
        return new ExchangeRate()
        {
            CompanyId = companyId,
            Date = ratesCouple.Date,
            EuroBuy = ratesCouple.Euro.Buy,
            EuroSell = ratesCouple.Euro.Sell,
            DollarBuy = ratesCouple.Dollar.Buy,
            DollarSell = ratesCouple.Dollar.Sell
        };
    }

    public AverageRate? BuildAverageRate(RatesCoupleDto ratesCouple)
    {
        return ratesCouple.Euro.Average.HasValue && ratesCouple.Dollar.Average.HasValue
        ? new AverageRate()
        {
            Date = ratesCouple.Date,
            Euro = ratesCouple.Euro.Average.Value,
            Dollar = ratesCouple.Dollar.Average.Value
        }
        : null;
    }
}
