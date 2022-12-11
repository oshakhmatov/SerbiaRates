using System.Globalization;
using System.Text.RegularExpressions;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public sealed partial class TackaParser : IRatesParser
{
	public ExchangeRateDto Parse(string input)
	{
		var dateNode = DateNodeRegex().Match(input).Value;
		var rawDate = DateRegex().Match(dateNode).Value;
		var date = DateOnly.ParseExact(rawDate, Const.DateFormat, CultureInfo.InvariantCulture);

		var currencyNodes = CurrencyNodeRegex()
			.Matches(input)
			.Select(m => m.Value)
			.ToArray();

		var euroBuy = PriceRegex().Match(currencyNodes[0]).Value;
		var euroSell = PriceRegex().Match(currencyNodes[1]).Value;
		var dollarBuy = PriceRegex().Match(currencyNodes[6]).Value;
		var dollarSell = PriceRegex().Match(currencyNodes[7]).Value;

		var russianCulture = new CultureInfo("ru-RU");

		return new ExchangeRateDto()
		{
			Date = date,
			Euro = new RateDto()
			{
				Buy = decimal.Parse(euroBuy, russianCulture),
				Sell = decimal.Parse(euroSell, russianCulture)
			},
			Dollar = new RateDto()
			{
				Buy = decimal.Parse(dollarBuy, russianCulture),
				Sell = decimal.Parse(dollarSell, russianCulture)
			}
		};
	}

	[GeneratedRegex(">Kurs&nbsp;ažuriran&nbsp;\\d{1,2}[.]\\d{1,2}[.]\\d{4}[.]&nbsp;godine<")]
	private static partial Regex DateNodeRegex();

	[GeneratedRegex("\\d{1,2}[.]\\d{1,2}[.]\\d{4}")]
	private static partial Regex DateRegex();

	[GeneratedRegex("<p class=\"kurs_p\">\\d{1,3}[,]\\d{2}</p>")]
	private static partial Regex CurrencyNodeRegex();

	[GeneratedRegex("\\d+[,]\\d+")]
	private static partial Regex PriceRegex();
}
