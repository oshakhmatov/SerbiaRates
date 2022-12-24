using SerbiaRates.Services.RatesUpdater.RateParsers;

namespace SerbiaRates.Services.RatesUpdater.ParserCreator;

public interface IParserCreator
{
    IRatesParser CreateParser(int providerId);
}
