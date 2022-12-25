using IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data;

namespace IntegrationTests;

public abstract class RepoTestBase : IClassFixture<DbFixture>
{
    private protected readonly AppDbContext dbContext;

    protected RepoTestBase(DbFixture fixture)
    {
        this.dbContext = fixture.DbContext;
    }
}
