using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class InfluencerVideo : IEntityTypeConfiguration<Domain.DbModel.InfluencerVideo>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.InfluencerVideo> builder)
	{
		builder = Core<Domain.DbModel.InfluencerVideo>.Configure(builder);
	}
}
