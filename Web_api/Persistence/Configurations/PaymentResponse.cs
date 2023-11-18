using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PaymentResponse : IEntityTypeConfiguration<Domain.DbModel.PaymentResponse>
{
    public void Configure(EntityTypeBuilder<Domain.DbModel.PaymentResponse> builder)
    {
        // builder = Core<Domain.PaymentResponse>.Configure(builder);
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.PaymentTry);
    }
}


