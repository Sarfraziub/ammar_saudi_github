using Domain.DbModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public  class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(x=> x.Key)
                .IsUnique();
            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}