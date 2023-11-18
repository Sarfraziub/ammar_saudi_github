using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class HomePageIcon : IEntityTypeConfiguration<Domain.DbModel.HomePageIcon>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.HomePageIcon> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.ArabicTitle)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(4000);
	}
}
