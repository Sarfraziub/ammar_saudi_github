using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class OrderSetting : IEntityTypeConfiguration<Domain.DbModel.OrderSetting>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.OrderSetting> builder)
	{
		builder.HasKey(e => e.Id);
	}
}
