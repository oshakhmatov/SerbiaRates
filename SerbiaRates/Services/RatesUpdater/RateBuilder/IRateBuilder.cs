using SerbiaRates.Models;
using SerbiaRates.Services.RatesUpdater;

namespace SerbiaRates.Services.RatesUpdater.RateBuilder
{
    public interface IRateBuilder
    {
        AverageRate? BuildAverageRate(RatesCoupleDto ratesCouple);
        ExchangeRate BuildExchangeRate(RatesCoupleDto ratesCouple, int companyId);
    }
}