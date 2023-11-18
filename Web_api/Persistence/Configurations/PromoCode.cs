using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PromoCode : IEntityTypeConfiguration<Domain.DbModel.PromoCode>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.PromoCode> builder)
	{
		builder = Core<Domain.DbModel.PromoCode>.Configure(builder);
		builder.HasMany(e => e.ClientOrders);
        builder.Property(x => x.ApplicableType)
            .HasDefaultValue(PromoCodeApplicableType.WebAndMobile);
    }
}


