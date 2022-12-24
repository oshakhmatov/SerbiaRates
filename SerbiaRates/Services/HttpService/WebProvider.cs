namespace SerbiaRates.Services.HttpService;

public sealed class WebProvider : IWebProvider
{
    private readonly IHttpClientFactory httpClientFactory;

    public WebProvider(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<string> Request(string url, CancellationToken token)
    {
        var client = httpClientFactory.CreateClient();

        return await client.GetStringAsync(url, token);
    }
}
