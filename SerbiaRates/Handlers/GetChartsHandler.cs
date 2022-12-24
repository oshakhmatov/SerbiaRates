using SerbiaRates.Data.Repos;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Handlers;

public sealed class GetChartsHandler
{
    private const int ChartDays = 30;

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
                Date = ar.Date.ToString("MMMM dd"),
                EUR = ar.Euro,
                USD = ar.Dollar
            })
            .ToArray()
        };
    }
}
