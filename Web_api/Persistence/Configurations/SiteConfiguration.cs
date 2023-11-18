using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Domain.DbModel.SiteConfiguration>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.SiteConfiguration> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(x => x.AndroidAppVersion)
                .HasMaxLength(100);
            builder.Property(x => x.IosAppVersion)
                .HasMaxLength(100);
            builder.Property(x => x.IsMaintenanceMode)
                .HasDefaultValue(false);
        }
    }
}