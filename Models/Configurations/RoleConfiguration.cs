using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // 需在中間表 RolePermissions 設定 ForeignKey, 可實現直接取得 role.permissions[]
        // 只需設一個，另一邊 ef 會自動產
        builder.HasMany(a => a.Permissions)
               .WithMany(r => r.Roles)
               .UsingEntity<RolePermission>(

                   // 要注意 join 順序
                   j => j.HasOne<Permission>().WithMany(r => r.RolePermissions).HasForeignKey(k => k.PermissionId),
                   j => j.HasOne<Role>().WithMany(p => p.RolePermissions).HasForeignKey(k => k.RoleId),
                   j => j.ToTable("RolePermissions"));

        builder.HasData(
            new Role()
            {
                Id = Guid.Parse("0a9e4508-0078-44f7-ae1e-ce07d65a84be"), Name = "Admin", NormalizedName = "Admin"
            },
            new Role()
            {
                Id = Guid.Parse("93779de5-302c-4dfc-ac5f-b3dccc155f3d"), Name = "Hmpy", NormalizedName = "基本權限"
            });
    }
}
