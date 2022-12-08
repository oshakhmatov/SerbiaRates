using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerbiaRates.Modules.ExchangeRates;

namespace SerbiaRates.Data.Configs;

public sealed class ExchangeRateConfig : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }
}
