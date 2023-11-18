using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SliderItem : IEntityTypeConfiguration<Domain.DbModel.SliderItem>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.SliderItem> builder)
	{
		builder = Core<Domain.DbModel.SliderItem>.Configure(builder);
	}
}


