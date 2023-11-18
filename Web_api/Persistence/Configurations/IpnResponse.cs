using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class IpnResponse : IEntityTypeConfiguration<Domain.DbModel.IpnResponse>
{
    public void Configure(EntityTypeBuilder<Domain.DbModel.IpnResponse> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.PaymentTry);
    }
}


