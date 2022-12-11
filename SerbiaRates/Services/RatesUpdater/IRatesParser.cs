namespace SerbiaRates.Services.RatesUpdater;

public interface IRatesParser
{
    public ExchangeRateDto Parse(string input);
}
