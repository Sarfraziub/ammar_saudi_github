using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserNotification : IEntityTypeConfiguration<Domain.DbModel.UserNotification>
{
    public void Configure(EntityTypeBuilder<Domain.DbModel.UserNotification> builder)
    {
        builder = Core<Domain.DbModel.UserNotification>.Configure(builder);
        builder.Property(x => x.CreatedBy)
            .IsRequired(false);
        builder.HasOne(e => e.User)
            .WithMany(m => m.UserNotifications)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            ;
    }
}