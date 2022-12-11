using Microsoft.EntityFrameworkCore;
using SerbiaRates.Models;

namespace SerbiaRates.Data;

public static class WebApplicationExtensions
{
    public static async Task MigrateAndSeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.Companies.AnyAsync())
        {
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
				},
				new Company
				{
					Id = Const.RaiffId,
					Name = "Raiffeisen",
					Url = "https://www.raiffeisenbank.rs/wp-json/wp/ea/currency?_wpnonce=ce5f7c1665&sdate="
				}
			});

            await dbContext.SaveChangesAsync();
        }
    }
}
