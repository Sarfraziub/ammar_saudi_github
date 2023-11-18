using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientOrderLog : IEntityTypeConfiguration<Domain.DbModel.ClientOrderLog>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.ClientOrderLog> builder)
	{
		builder.HasKey(e => e.Id);

		builder.HasOne(e => e.ClientOrder)
			.WithMany(m => m.ClientOrderLogs)
			.HasForeignKey(f => f.ClientOrderId).OnDelete(DeleteBehavior.Restrict);
	}
}


