using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserDeviceToken : IEntityTypeConfiguration<Domain.DbModel.UserDeviceToken>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.UserDeviceToken> builder)
	{
		builder = Core<Domain.DbModel.UserDeviceToken>.Configure(builder);
		builder.HasOne(e => e.User)
			.WithMany(m => m.UserDeviceTokens)
			.HasForeignKey(f => f.UserId)
			.OnDelete(DeleteBehavior.Restrict)
			;
	}
}


