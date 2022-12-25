namespace IntegrationTests;

public abstract class ApiTestBase
{
    private static protected readonly HttpClient client = new WebAppFactory().CreateClient();
}
