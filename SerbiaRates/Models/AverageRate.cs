namespace SerbiaRates.Models;

public sealed class AverageRate
{
    public int Id { get; init; }
    public required DateOnly Date { get; init; }
    public required decimal Euro { get; init; }
    public required decimal Dollar { get; init; }
}
