using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class File : IEntityTypeConfiguration<Domain.DbModel.File>
{
	public void Configure(EntityTypeBuilder<Domain.DbModel.File> builder)
	{
		builder = Core<Domain.DbModel.File>.Configure(builder);
		builder.HasMany(e => e.SaleItems)
			.WithOne(m => m.Image)
			.HasForeignKey(f => f.ImageId);

		builder.HasMany(e => e.ApplicationUsers)
			.WithOne(m => m.Image)
			.HasForeignKey(f => f.ImageId).OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(e => e.SliderItems)
			.WithOne(m => m.Image)
			.HasForeignKey(f => f.ImageId).OnDelete(DeleteBehavior.Restrict);


		builder.HasMany(e => e.NationalImages)
			.WithOne(m => m.NationalImage)
			.HasForeignKey(f => f.NationalImageImageId).OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(e => e.IbanImages)
			.WithOne(m => m.IbanImage)
			.HasForeignKey(f => f.IbanImageId).OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(e => e.DriverClaims)
			.WithOne(m => m.Receipt)
			.HasForeignKey(f => f.ReceiptId).OnDelete(DeleteBehavior.Restrict);


		builder.HasMany(e => e.HomePageIcons)
			.WithOne(m => m.File)
			.HasForeignKey(f => f.FileId).OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(e => e.InfluencerVideos)
			.WithOne(m => m.File)
			.HasForeignKey(f => f.FileId).OnDelete(DeleteBehavior.Restrict);


		builder.HasMany(e => e.Packages)
			.WithOne(m => m.File)
			.HasForeignKey(f => f.FileId);


        builder.HasMany(e => e.ContentSettings)
            .WithOne(m => m.Image)
            .HasForeignKey(f => f.ImageId).OnDelete(DeleteBehavior.Restrict);

    }
}


