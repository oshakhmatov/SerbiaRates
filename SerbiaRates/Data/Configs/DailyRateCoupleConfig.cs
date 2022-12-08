using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerbiaRates.Modules.ExchangeRates;

namespace SerbiaRates.Data.Configs;

public sealed class DailyRateCoupleConfig : IEntityTypeConfiguration<DailyRateCouple>
{
    public void Configure(EntityTypeBuilder<DailyRateCouple> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }
}
