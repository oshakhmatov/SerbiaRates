using SerbiaRates.Services.RatesUpdater;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public interface IRatesParser
{
    public RatesCoupleDto Parse(string input);
}
