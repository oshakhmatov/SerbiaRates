using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerbiaRates.Services.RatesUpdater.RateParsers;

public class PostanskaRateParser : IRatesParser
{
    private const int DollarId = 840;
    private const int EuroId = 978;

    public ExchangeRateDto Parse(string input)
    {
        var ratesResult = JsonSerializer.Deserialize<RatesResult>(input);

        var rates = ratesResult.Result.Rates.Select(r => new ParsedRate()
        {
            Date = DateOnly.ParseExact(r.Date?.Trim('.'), Const.DateFormat, CultureInfo.InvariantCulture),
            CurrencyId = int.Parse(r.CurrencyId.ToString()),
            Buy = decimal.Parse(r.Buy.ToString(), CultureInfo.InvariantCulture),
            Sell = decimal.Parse(r.Sell.ToString(), CultureInfo.InvariantCulture)
        });

        var date = rates.FirstOrDefault()!.Date;
        var dollar = rates.FirstOrDefault(r => r.CurrencyId == DollarId);
        var euro = rates.FirstOrDefault(r => r.CurrencyId == EuroId);

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
}

public sealed record ParsedRate
{
    public required int CurrencyId { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal Buy { get; init; }
    public required decimal Sell { get; init; }
}

public sealed class RatesResult
{
    [JsonPropertyName("Kursna_lista")]
    public Result? Result { get; set; }
}

public class Result
{
    [JsonPropertyName("Valuta")]
    public Rate[]? Rates { get; set; }
}

public class Rate
{
    [JsonPropertyName("Vreme")]
    public string? Date { get; set; }
    [JsonPropertyName("Sifra_valute")]
    public object? CurrencyId { get; set; }
    [JsonPropertyName("Klkup_kurs")]
    public object? Buy { get; set; }
    [JsonPropertyName("Prodajni_kurs")]
    public object? Sell { get; set; }
}
