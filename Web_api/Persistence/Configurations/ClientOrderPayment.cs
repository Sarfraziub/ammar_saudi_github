using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientOrderPayment : IEntityTypeConfiguration<Domain.DbModel.ClientOrderPayment>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.ClientOrderPayment> builder)
	{
		builder = Core<Domain.DbModel.ClientOrderPayment>.Configure(builder);

        builder.Property(x => x.CreatedBy)
            .IsRequired(false);
        builder.Property(x => x.CartId)
            .HasMaxLength(250);
        builder.Property(x => x.TransactionReference)
            .HasMaxLength(250);

		builder.HasOne(e => e.ClientOrder)
			.WithMany(m => m.ClientOrderPayments)
			.HasForeignKey(f => f.ClientOrderId).OnDelete(DeleteBehavior.Restrict);

	}
}
