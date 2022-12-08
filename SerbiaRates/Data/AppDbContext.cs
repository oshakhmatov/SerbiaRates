using Microsoft.EntityFrameworkCore;
using SerbiaRates.Modules.ExchangeRates;
using SerbiaRates.Modules.Shared;
using System.Reflection;

namespace SerbiaRates.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<DailyRateCouple> DailyRateCouples { get; set; }
	public DbSet<DailyAverageRateCouple> DailyAverageRateCouples { get; set; }
	public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
