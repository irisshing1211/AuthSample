using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.Models.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        builder.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
        builder.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId);
    }
}
