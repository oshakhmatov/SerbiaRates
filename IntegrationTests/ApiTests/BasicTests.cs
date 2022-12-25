namespace IntegrationTests.ApiTests;

public sealed class BasicTests : ApiTestBase
{
    [Theory]
    [InlineData("/rates")]
    [InlineData("/charts")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
