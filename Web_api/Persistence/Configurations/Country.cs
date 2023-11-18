using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Country : IEntityTypeConfiguration<Domain.DbModel.Country>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.Country> builder)
	{
		builder.HasKey(e => e.Id);
		builder.HasMany(e => e.Users)
			.WithOne(m => m.Country)
			.HasForeignKey(f => f.CountryId);
	}
}

