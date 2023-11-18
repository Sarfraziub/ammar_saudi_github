using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientOrderDeliverImage : IEntityTypeConfiguration<Domain.DbModel.ClientOrderDeliverImage>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.ClientOrderDeliverImage> builder)
	{
		builder = Core<Domain.DbModel.ClientOrderDeliverImage>.Configure(builder);
		builder.HasOne(e => e.ClientOrder)
			.WithMany(m => m.ClientOrderDeliverImages)
			.HasForeignKey(f => f.ClientOrderId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.File)
			.WithMany(m => m.ClientOrderDeliverImages)
			.HasForeignKey(f => f.FileId).OnDelete(DeleteBehavior.Restrict);

	}
}
