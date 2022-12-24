using SerbiaRates.Services.RateParsers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public sealed partial class EldoradoParser : IRatesParser
{
    public RatesCoupleDto Parse(string input)
    {
        var dateNode = DateNodeRegex().Match(input).Value;
        var rawDate = DateRegex().Match(dateNode).Value;
        var date = DateOnly.ParseExact(rawDate, Const.DateFormat, CultureInfo.InvariantCulture);

        var currencyNodes = CurrencyNodeRegex()
            .Matches(input)
            .Select(m => m.Value)
            .ToArray();

        var euroBuy = PriceRegex().Match(currencyNodes[0]).Value;
        var euroSell = PriceRegex().Match(currencyNodes[2]).Value;
        var dollarBuy = PriceRegex().Match(currencyNodes[3]).Value;
        var dollarSell = PriceRegex().Match(currencyNodes[5]).Value;

        return new RatesCoupleDto()
        {
            Date = date,
            Euro = new RateDto()
            {
                Buy = decimal.Parse(euroBuy),
                Sell = decimal.Parse(euroSell)
            },
            Dollar = new RateDto()
            {
                Buy = decimal.Parse(dollarBuy),
                Sell = decimal.Parse(dollarSell)
            }
        };
    }

    [GeneratedRegex(">Kursna lista na dan \\d{1,2}[.]\\d{1,2}[.]\\d{4}[.]<")]
    private static partial Regex DateNodeRegex();

    [GeneratedRegex("\\d{1,2}[.]\\d{1,2}[.]\\d{4}")]
    private static partial Regex DateRegex();

    [GeneratedRegex("<td class=\"tdright\">\\d{1,3}[.]\\d{4}</td>")]
    private static partial Regex CurrencyNodeRegex();

    [GeneratedRegex("\\d+[.]\\d+")]
    private static partial Regex PriceRegex();
}
