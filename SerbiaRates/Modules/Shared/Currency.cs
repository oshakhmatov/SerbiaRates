using SerbiaRates.Modules.ExchangeRates;

namespace SerbiaRates.Modules.Shared;

public class Currency
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public List<ExchangeRate>? ExchangeRates { get; set; }
}