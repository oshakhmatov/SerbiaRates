namespace SerbiaRates.Services.RatesUpdater;

public interface IRatesUpdater
{
    Task UpdateRates(CancellationToken token);
}