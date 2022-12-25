namespace SerbiaRates.Data.Abstractions;

public interface IDbInitializer
{
    Task MigrateAndSeedDatabase();
}
