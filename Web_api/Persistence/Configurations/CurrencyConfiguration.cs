using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Domain.DbModel.Currency>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.Currency> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.Property(x => x.Name)
                .HasMaxLength(50);
            builder.Property(x => x.Code)
                .HasMaxLength(10);
        }
    }
}
