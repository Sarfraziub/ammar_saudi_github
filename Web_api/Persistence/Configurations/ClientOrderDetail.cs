using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientOrderDetail : IEntityTypeConfiguration<Domain.DbModel.ClientOrderDetail>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.ClientOrderDetail> builder)
	{
		// builder = Core<Domain.ClientOrderDetail>.Configure(builder);
		builder.HasKey(e => e.Id);

		builder.Property(e => e.Price)
			.HasColumnType("money")
			.IsRequired();

		builder.HasOne(e => e.ClientOrder)
			.WithMany(m => m.ClientOrderDetails)
			.HasForeignKey(f => f.ClientOrderId).OnDelete(DeleteBehavior.Restrict);

		// builder.HasIndex(a => new { a.SaleItemId, a.ClientOrderId }).IsUnique();
	}
}
