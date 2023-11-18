using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class NotificationTemplate : IEntityTypeConfiguration<Domain.DbModel.NotificationTemplate>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.NotificationTemplate> builder)
	{
		builder = Core<Domain.DbModel.NotificationTemplate>.Configure(builder);
	}
}


