using IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SerbiaRates.Data;
using SerbiaRates.Data.Abstractions;

namespace IntegrationTests.Fixtures;

public sealed class WebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            var dbContextDescriptor = services
                .FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            services.Remove(dbContextDescriptor);

            //services.AddDbContext<AppDbContext>(options => options
            //    .UseNpgsql(context.Configuration["TestDbRates"]));

            services.AddDbContext<AppDbContext>(
                o => o.UseInMemoryDatabase("TestRates"));

            var dbInitializerDescriptor = services
                .FirstOrDefault(d => d.ServiceType == typeof(IDbInitializer));

            services.Remove(dbInitializerDescriptor);

            services.AddScoped<IDbInitializer, MockDbInitializer>();
        });
    }
}