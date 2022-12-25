using SerbiaRates.Data.Abstractions;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Handlers;

public sealed class GetChartsHandler
{
    private const int ChartDays = 30;
    private const string DateFormat = "MMMM dd";

    private readonly IRepo ratesRepo;

    public GetChartsHandler(IRepo ratesRepo)
    {
        this.ratesRepo = ratesRepo;
    }

    public async Task<ChartsViewModel?> Handle(CancellationToken token)
    {
        var averageRates = await ratesRepo.GetAverageRates(take: ChartDays, token);
        if (!averageRates.Any())
            return null;

        return new ChartsViewModel()
        {
            Points = averageRates.Select(ar => new Point()
            {
                Date = ar.Date.ToString(DateFormat),
                EUR = ar.Euro,
                USD = ar.Dollar
            })
            .ToArray()
        };
    }
}
