using SerbiaRates.Services.RateParsers;

namespace SerbiaRates.Services.ParserCreator;

public interface IParserCreator
{
    IRatesParser CreateParser(int providerId);
}
