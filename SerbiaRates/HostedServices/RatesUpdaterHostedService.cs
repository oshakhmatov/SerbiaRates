using SerbiaRates.Services.RatesUpdater;

namespace SerbiaRates.HostedServices;

public sealed class RatesUpdaterHostedService : HostedServiceBase
{
    private const int IntervalHours = 1;

    private readonly IServiceProvider serviceProvider;

    public RatesUpdaterHostedService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    protected override TimeSpan Interval => TimeSpan.FromHours(IntervalHours);

    protected override async Task DoWork(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var ratesUpdater = scope.ServiceProvider.GetRequiredService<IRatesUpdater>();

        await ratesUpdater.UpdateRates(stoppingToken);
    }
}
