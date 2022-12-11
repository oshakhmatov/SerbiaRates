using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerbiaRates.Models;

namespace SerbiaRates.Data.Configs;

public sealed class ExchangeRateConfig : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasOne(er => er.Company)
            .WithMany();
    }
}
