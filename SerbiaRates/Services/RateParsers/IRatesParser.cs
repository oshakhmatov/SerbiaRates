using SerbiaRates.Services.RatesUpdater;

namespace SerbiaRates.Services.RateParsers;

public interface IRatesParser
{
    public RatesCoupleDto Parse(string input);
}
