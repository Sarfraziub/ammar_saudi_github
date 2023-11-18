using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DbModel;

namespace Persistence.Configurations
{
    public class PromotionalLinkConfiguration : IEntityTypeConfiguration<PromotionalLink>
    {
        public void Configure(EntityTypeBuilder<PromotionalLink> builder)
        {
            builder = Core<PromotionalLink>.Configure(builder);
            builder.HasIndex(x=> x.UniqueId)
                .IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(120);
            builder.Property(e => e.UniqueId)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(e => e.MobileNo)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(50);
            builder.Property(e => e.UniqueName)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(e => e.Status)
                .HasDefaultValue(true);
        }
    }
}