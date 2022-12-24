namespace SerbiaRates.Services.WebProvider;

public interface IWebProvider
{
    Task<string> Request(string url, CancellationToken token);
}