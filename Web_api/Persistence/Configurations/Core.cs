using Domain;
using Domain.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class Core<T> where T : Entity
{
	public static EntityTypeBuilder<T> Configure(EntityTypeBuilder<T> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Created)
			.HasColumnType("datetime")
			.IsRequired();

		builder.Property(e => e.CreatedBy)
			.HasColumnType("nvarchar(200)")
			.IsRequired();

		builder.Property(e => e.Updated)
			.HasColumnType("datetime");

		builder.Property(e => e.UpdatedBy)
			.HasColumnType("nvarchar(200)");
		return builder;
	}
}


