namespace SerbiaRates.Modules.ExchangeRates.RatesUpdater;

public interface IRatesParser
{
    public DailyRateCoupleDto Parse(string input);
}
