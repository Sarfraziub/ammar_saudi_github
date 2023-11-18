using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class ActionTrackingHistory : IEntityTypeConfiguration<Domain.DbModel.ActionTrackingHistory>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.ActionTrackingHistory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.DeviceId)
                .HasMaxLength(250);
            builder.Property(x => x.Ip)
                .HasMaxLength(50);
            builder.Property(x => x.Platform)
                .HasMaxLength(50);
        }
    }
}
