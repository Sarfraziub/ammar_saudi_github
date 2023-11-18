using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class DriverClaim : IEntityTypeConfiguration<Domain.DbModel.DriverClaim>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.DriverClaim> builder)
	{
		builder = Core<Domain.DbModel.DriverClaim>.Configure(builder);
		builder.HasOne(e => e.Driver)
			.WithMany(m => m.DriverClaims)
			.HasForeignKey(f => f.DriverId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.Receipt)
			.WithMany(m => m.DriverClaims)
			.HasForeignKey(f => f.ReceiptId).OnDelete(DeleteBehavior.Restrict);


	}
}
