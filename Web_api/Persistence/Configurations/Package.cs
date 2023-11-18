using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Package : IEntityTypeConfiguration<Domain.DbModel.Package>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.Package> builder)
	{
		builder = Core<Domain.DbModel.Package>.Configure(builder);

	}
}
