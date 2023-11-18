using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SaleItem : IEntityTypeConfiguration<Domain.DbModel.SaleItem>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.SaleItem> builder)
	{
		builder = Core<Domain.DbModel.SaleItem>.Configure(builder);
		builder.Property(e => e.Price)
			.HasColumnType("money")
			.IsRequired();

		builder.Property(e => e.Name)
			.IsRequired()
			.IsUnicode(false)
			.HasMaxLength(120);
		builder.Property(e => e.ArabicName)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(120);
		builder.Property(e => e.Specifications)
			.IsRequired()
			.IsUnicode(false)
			.HasMaxLength(500);
		builder.Property(e => e.ArabicSpecifications)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(500);
        builder.Property(e => e.SalesItemQuantity)
            .HasDefaultValue(1);

        builder.HasOne(e => e.Package)
			.WithMany(m => m.SaleItems)
			.HasForeignKey(f => f.PackageId).OnDelete(DeleteBehavior.Restrict);

	}
}


