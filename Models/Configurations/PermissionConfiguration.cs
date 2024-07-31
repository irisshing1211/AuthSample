using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.Models.Configurations;

public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
{

    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.Code).IsUnique();
    }
}
