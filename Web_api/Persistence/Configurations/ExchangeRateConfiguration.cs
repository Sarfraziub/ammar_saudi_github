using Domain.DbModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(x => x.FromCurrencyId)
                .IsUnique(false);
            builder.HasIndex(x => x.ToCurrencyId)
                .IsUnique(false);
            builder.Property(x => x.Rate)
                .HasPrecision(18, 6);

            builder.HasOne(e => e.FromCurrency)
                .WithMany(m => m.FromCurrencyExchangeRate)
                .HasForeignKey(x => x.FromCurrencyId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.ToCurrency)
                .WithMany(m => m.ToCurrencyExchangeRate)
                .HasForeignKey(x => x.ToCurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
