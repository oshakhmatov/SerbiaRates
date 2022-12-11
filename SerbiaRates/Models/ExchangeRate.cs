namespace SerbiaRates.Models;

public sealed class ExchangeRate
{
    public int Id { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal EuroSell { get; init; }
    public required decimal EuroBuy { get; init; }
    public required decimal DollarSell { get; init; }
    public required decimal DollarBuy { get; init; }

    public DateOnly CreateDate { get; init; } = DateOnly.FromDateTime(DateTime.Today);

    public required int CompanyId { get; init; }
    public Company? Company { get; init; }
}
