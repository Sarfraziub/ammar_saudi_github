using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PaymentTry : IEntityTypeConfiguration<Domain.DbModel.PaymentTry>
{
    public void Configure(EntityTypeBuilder<Domain.DbModel.PaymentTry> builder)
    {
        builder = Core<Domain.DbModel.PaymentTry>.Configure(builder);
        builder.Property(x => x.CreatedBy)
            .IsRequired(false);
        builder.HasOne(e => e.ClientOrder)
            .WithMany(m => m.PaymentTries)
            .HasForeignKey(f => f.ClientOrderId);
    }
}


