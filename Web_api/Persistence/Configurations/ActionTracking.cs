using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class ActionTracking : IEntityTypeConfiguration<Domain.DbModel.ActionTracking>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.ActionTracking> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .HasMaxLength(250);
        }
    }
}
