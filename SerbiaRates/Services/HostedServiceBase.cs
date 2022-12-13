namespace SerbiaRates.Services;

public abstract class HostedServiceBase : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DoWork(stoppingToken);

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    protected abstract TimeSpan Interval { get; }

    protected abstract ValueTask DoWork(CancellationToken stoppingToken);
}
