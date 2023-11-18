using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class DriverFee : IEntityTypeConfiguration<Domain.DbModel.DriverFee>
{
    public void Configure(EntityTypeBuilder<Domain.DbModel.DriverFee> builder)
    {
        builder = Core<Domain.DbModel.DriverFee>.Configure(builder);    
    }
}