namespace SerbiaRates.HostedServices;

public abstract class HostedServiceBase : BackgroundService
{
    protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DoWork(stoppingToken);

                await Task.Delay(Interval, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    protected abstract TimeSpan Interval { get; }

    protected abstract Task DoWork(CancellationToken stoppingToken);
}
