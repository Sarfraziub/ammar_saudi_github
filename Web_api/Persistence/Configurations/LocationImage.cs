using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LocationImage : IEntityTypeConfiguration<Domain.DbModel.LocationImage>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.LocationImage> builder)
	{
		builder = Core<Domain.DbModel.LocationImage>.Configure(builder);

		builder.HasOne(e => e.File)
			.WithMany(m => m.LocationImages)
			.HasForeignKey(f => f.FileId).OnDelete(DeleteBehavior.Restrict);


		builder.HasOne(e => e.Location)
			.WithMany(m => m.LocationImages)
			.HasForeignKey(f => f.LocationId).OnDelete(DeleteBehavior.Restrict);
		builder.ToTable("LocationImages");

	}
}
