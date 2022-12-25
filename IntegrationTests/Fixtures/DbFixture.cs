using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data;

namespace IntegrationTests.Fixtures;

public class DbFixture
{
    public DbFixture()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        DbContext = new AppDbContext(optionsBuilder.Options);
    }

    public AppDbContext DbContext { get; }
}
