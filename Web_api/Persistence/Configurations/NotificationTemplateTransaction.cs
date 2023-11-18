using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class NotificationTemplateTransaction : IEntityTypeConfiguration<Domain.DbModel.NotificationTemplateTransaction>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.NotificationTemplateTransaction> builder)
	{
		builder = Core<Domain.DbModel.NotificationTemplateTransaction>.Configure(builder);
		builder.HasOne(e => e.NotificationTemplate)
			.WithMany(m => m.NotificationTemplateTransactions)
			.HasForeignKey(f => f.NotificationTemplateId)
			.OnDelete(DeleteBehavior.Restrict)
			;
	}
}

