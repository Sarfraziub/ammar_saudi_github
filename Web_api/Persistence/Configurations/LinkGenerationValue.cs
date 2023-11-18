using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    internal class LinkGenerationValue : IEntityTypeConfiguration<Domain.DbModel.LinkGenerationValue>
    {
        public void Configure(EntityTypeBuilder<Domain.DbModel.LinkGenerationValue> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.Type)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(x => x.Value)
                .IsRequired();
        }
    }
}
