using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Region : IEntityTypeConfiguration<Domain.DbModel.Region>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.Region> builder)
	{
		builder = Core<Domain.DbModel.Region>.Configure(builder);
		builder.Property(e => e.Name)
			.IsRequired()
			.IsUnicode(false)
			.HasMaxLength(120);
		builder.Property(e => e.ArabicName)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(120);
		builder.HasMany(e => e.Locations);
	}
}


