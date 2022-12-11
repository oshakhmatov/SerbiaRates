using Microsoft.EntityFrameworkCore;
using SerbiaRates.Models;
using System.Reflection;

namespace SerbiaRates.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Company> Companies { get; set; }
	public DbSet<AverageRate> AverageRates { get; set; }   
	public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
