namespace SerbiaRates.Services.HttpService;

public interface IWebProvider
{
    Task<string> Request(string url, CancellationToken token);
}