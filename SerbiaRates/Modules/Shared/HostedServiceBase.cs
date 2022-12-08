namespace SerbiaRates.Modules.Shared;

public abstract class HostedServiceBase : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWork(stoppingToken);

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    protected abstract TimeSpan Interval { get; }

    protected abstract ValueTask DoWork(CancellationToken stoppingToken);
}
