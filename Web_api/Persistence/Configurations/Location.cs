using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Location : IEntityTypeConfiguration<Domain.DbModel.Location>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.Location> builder)
	{
		builder = Core<Domain.DbModel.Location>.Configure(builder);
		builder.Property(e => e.Name)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(120);
		builder.Property(e => e.ArabicName)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(120);
		builder.Property(e => e.Latitude)
			.IsRequired();
		builder.Property(e => e.Longitude)
			.IsRequired();
		builder.HasOne(e => e.Region);
		builder.Property(e => e.Description)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(200);
		builder.Property(e => e.ArabicDescription)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(200);
	}
}


