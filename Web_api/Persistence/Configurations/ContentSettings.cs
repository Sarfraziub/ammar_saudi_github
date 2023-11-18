using Domain;
using Domain.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ContentSettings : IEntityTypeConfiguration<ContentSetting>
{
	public void Configure(EntityTypeBuilder<ContentSetting> builder)
	{
		builder = Core<ContentSetting>.Configure(builder);
        builder.HasIndex(x => x.Key)
            .IsUnique();
		builder.Property(e => e.ArabicContent)
			.IsRequired()
			.IsUnicode()
			.HasColumnType("nvarchar(max)");
        builder.Property(x => x.Key)
            .IsRequired()
            .HasDefaultValue(Guid.NewGuid().ToString());
    }
}
