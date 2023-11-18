using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class LinkGeneration : IEntityTypeConfiguration<Domain.DbModel.LinkGeneration>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.LinkGeneration> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.UniqueId)
                .IsUnique();
            builder.Property(x => x.UniqueId)
                .HasMaxLength(50);
            builder.Property(x => x.IsValid)
                .HasDefaultValue(true);
        }
    }
}