using SerbiaRates.Data.Abstractions;

namespace SerbiaRates.Data;

public static class WebApplicationExtensions
{
    public static async Task MigrateAndSeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        await dbInitializer.MigrateAndSeedDatabase();
    }
}
