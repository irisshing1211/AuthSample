using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.Models.Configurations;

public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
{

    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(a => a.Rd);
        builder.HasIndex(a => a.Code).IsUnique();

        // builder.HasData(new List<Permission>()
        // {
        //     new Permission() { Id = 1, Code = "ModuleA_FuncA_Read" },
        //     new Permission() { Id = 2, Code = "ModuleA_FuncA_Add" },
        //     new Permission() { Id = 3, Code = "ModuleA_FuncB_List" },
        //     new Permission() { Id = 4, Code = "ModuleB_FuncA_List" }
        // });
    }
}
