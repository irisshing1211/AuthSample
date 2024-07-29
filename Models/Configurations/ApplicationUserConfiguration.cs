using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.Models.Configurations;

public class ApplicationUserConfiguration:IEntityTypeConfiguration<ApplicationUser>
{

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // 需在中間表 UserRole 設定 ForeignKey, 可實現直接取得 user.roles[]
        // 只需設一個，另一邊 ef 會自動產
        builder.HasMany(a=>a.Roles).WithMany(r => r.Users)
               .UsingEntity<UserRole>(
                   j => j.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId),
                   j => j.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId),
                   j => j.ToTable("UserRoles"));
    }
}
