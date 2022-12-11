using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public class RaiffParser : IRatesParser
{
	private const int DollarId = 840;
	private const int EuroId = 978;
	private const string DateFormat = "yyyy.MM.dd";

	public ExchangeRateDto Parse(string input)
	{
		var rates = JsonSerializer.Deserialize<Rate[]>(input);

		var parsedRates = rates!.Select(r => new ParsedRate()
		{
			Date = DateOnly.ParseExact(r.Date!, DateFormat, CultureInfo.InvariantCulture),
			CurrencyId = int.Parse(r.CurrencyId!.ToString()),
			Buy = r.Buy!.Value,
			Sell = r.Sell!.Value
		});

		var date = parsedRates.FirstOrDefault()!.Date;
		var dollar = parsedRates.FirstOrDefault(r => r.CurrencyId == DollarId);
		var euro = parsedRates.FirstOrDefault(r => r.CurrencyId == EuroId);

		return new ExchangeRateDto()
		{
			Date = date,
			Euro = new RateDto()
			{
				Buy = euro!.Buy,
				Sell = euro!.Sell
			},
			Dollar = new RateDto()
			{
				Buy = dollar!.Buy,
				Sell = dollar!.Sell
			}
		};
	}

	public sealed record ParsedRate
	{
		public required int CurrencyId { get; init; }
		public required DateOnly Date { get; init; }
		public required decimal Buy { get; init; }
		public required decimal Sell { get; init; }
	}

	public class Rate
	{
		[JsonPropertyName("dateList")]
		public string? Date { get; set; }
		[JsonPropertyName("currencyId")]
		public string? CurrencyId { get; set; }
		[JsonPropertyName("buyCashe")]
		public decimal? Buy { get; set; }
		[JsonPropertyName("sellCashe")]
		public decimal? Sell { get; set; }
	}
}
