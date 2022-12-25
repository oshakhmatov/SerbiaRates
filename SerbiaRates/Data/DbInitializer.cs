using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data.Abstractions;
using SerbiaRates.Models;

namespace SerbiaRates.Data;

public sealed class DbInitializer : IDbInitializer
{
    private readonly AppDbContext dbContext;

    public DbInitializer(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task MigrateAndSeedDatabase()
    {
        await dbContext.Database.MigrateAsync();

        if (await dbContext.Companies.AnyAsync())
            return;

        dbContext.AddRange(new[]
        {
            new Company
            {
                Id = Const.GagaId,
                Name = "Gaga",
                Url = "https://menjacnicegaga.rs/"
            },
            new Company
            {
                Id = Const.PostanskaId,
                Name = "Postal Savings Bank",
                Url = "https://www.posted.co.rs/testang/phpremote/jsonall.php"
            },
            new Company
            {
                Id = Const.EldoradoId,
                Name = "Eldorado",
                Url = "https://www.eldorado-exchange.rs/kursna-lista/"
            },
            new Company
            {
                Id = Const.TackaId,
                Name = "Tačka",
                Url = "https://www.menjacnicatacka.co.rs/"
            }
        });

        await dbContext.SaveChangesAsync();
    }
}
