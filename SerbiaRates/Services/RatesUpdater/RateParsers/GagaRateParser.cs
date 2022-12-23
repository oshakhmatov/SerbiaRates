using System.Globalization;
using System.Text.RegularExpressions;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public sealed partial class GagaRateParser : IRatesParser
{
    public ExchangeRateDto Parse(string input)
    {
        var dateNode = DateNodeRegex().Match(input).Value;
        var rawDate = DateRegex().Match(dateNode).Value;
        var date = DateOnly.ParseExact(rawDate, Const.DateFormat, CultureInfo.InvariantCulture);

        var euroNode = EuroNodeRegex().Match(input).Value;
        var euroBuyNode = BuyNodeRegex().Match(euroNode).Value;
        var eurorAverageNode = AverageNodeRegex().Match(euroNode).Value;
        var euroSellNode = SellNodeRegex().Match(euroNode).Value;
        var euroBuy = PriceRegex().Match(euroBuyNode).Value;
        var euroAverage = PriceRegex().Match(eurorAverageNode).Value;
        var euroSell = PriceRegex().Match(euroSellNode).Value;

        var dollarNode = DollarNodeRegex().Match(input).Value;
        var dollarBuyNode = BuyNodeRegex().Match(dollarNode).Value;
        var dollarAverageNode = AverageNodeRegex().Match(dollarNode).Value;
        var dollarSellNode = SellNodeRegex().Match(dollarNode).Value;
        var dollarBuy = PriceRegex().Match(dollarBuyNode).Value;
        var dollarAverage = PriceRegex().Match(dollarAverageNode).Value;
        var dollarSell = PriceRegex().Match(dollarSellNode).Value;

        var russianCulture = new CultureInfo("ru-RU");

        return new ExchangeRateDto()
        {
            Date = date,
            Euro = new RateDto()
            {
                Buy = decimal.Parse(euroBuy, russianCulture),
                Sell = decimal.Parse(euroSell, russianCulture),
                Average = decimal.Parse(euroAverage, russianCulture)
            },
            Dollar = new RateDto()
            {
                Buy = decimal.Parse(dollarBuy, russianCulture),
                Sell = decimal.Parse(dollarSell, russianCulture),
                Average = decimal.Parse(dollarAverage, russianCulture)
            }
        };
    }

    [GeneratedRegex(">Kursna lista poslednji put ažurirana dana \\d{1,2}[.]\\d{1,2}[.]\\d{4}[.]<")]
    private static partial Regex DateNodeRegex();

    [GeneratedRegex("\\d{1,2}[.]\\d{1,2}[.]\\d{4}")]
    private static partial Regex DateRegex();

    [GeneratedRegex("EUR<\\/td><td class=\"column-4\">1<\\/td><td class=\"column-5\">\\d+,\\d+<\\/td><td class=\"column-6\">\\d+,\\d+<\\/td><td class=\"column-7\">\\d+,\\d+<\\/td>")]
    private static partial Regex EuroNodeRegex();

    [GeneratedRegex("USD<\\/td><td class=\"column-4\">1<\\/td><td class=\"column-5\">\\d+,\\d+<\\/td><td class=\"column-6\">\\d+,\\d+<\\/td><td class=\"column-7\">\\d+,\\d+<\\/td>")]
    private static partial Regex DollarNodeRegex();

    [GeneratedRegex("class=\"column-5\">\\d+,\\d+<")]
    private static partial Regex BuyNodeRegex();

    [GeneratedRegex("class=\"column-6\">\\d+,\\d+<")]
    private static partial Regex AverageNodeRegex();

    [GeneratedRegex("class=\"column-7\">\\d+,\\d+<")]
    private static partial Regex SellNodeRegex();

    [GeneratedRegex("\\d+,\\d+")]
    private static partial Regex PriceRegex();
}
