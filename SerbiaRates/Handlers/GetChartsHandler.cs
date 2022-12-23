using SerbiaRates.Data.Repos;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Handlers;

public sealed class GetChartsHandler
{
    private readonly IRepo ratesRepo;

    public GetChartsHandler(IRepo ratesRepo)
    {
        this.ratesRepo = ratesRepo;
    }

    public async Task<ChartsViewModel?> Handle()
    {
        var averageRates = await ratesRepo.GetAverageRates();
        if (averageRates is null)
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
