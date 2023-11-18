using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Otp : IEntityTypeConfiguration<Domain.DbModel.Otp>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.Otp> builder)
	{
		builder.HasKey(e => e.Id);
	}
}
