using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientOrder : IEntityTypeConfiguration<Domain.DbModel.ClientOrder>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.ClientOrder> builder)
	{
		// builder = Core<Domain.ClientOrder>.Configure(builder);
		builder.HasKey(e => e.Id);

        builder.Property(x => x.DeviceSource)
            .HasMaxLength(50);
		builder.Property(x => x.PromoCodeAppliedSource)
            .HasMaxLength(50);
		builder.Property(x => x.ExchangeRate)
            .HasDefaultValue(1);

		builder.HasOne(e => e.Driver)
			.WithMany(m => m.DriverClientOrders)
			.HasForeignKey(f => f.DriverId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.Client)
			.WithMany(m => m.ClientOrders)
			.HasForeignKey(f => f.ClientId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.Location)
			.WithMany(m => m.ClientOrders)
			.HasForeignKey(f => f.LocationId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.DriverFee)
			.WithMany(m => m.ClientOrders)
			.HasForeignKey(f => f.DriverFeeId).OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(e => e.DriverClaim)
			.WithMany(m => m.ClientOrders)
			.HasForeignKey(f => f.DriverClaimId).OnDelete(DeleteBehavior.Restrict);


	}
}


