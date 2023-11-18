using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class Gift : IEntityTypeConfiguration<Domain.DbModel.Gift>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.Gift> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.ReceiverName)
                .IsRequired()
                .HasMaxLength(250);
            builder.HasKey(e => e.Id);
            builder.Property(x => x.SenderName)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
