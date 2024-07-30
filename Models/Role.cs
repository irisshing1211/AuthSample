using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace AuthSample.Models;

public class Role : IdentityRole<Guid>
{
    public ICollection<ApplicationUser> Users { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}

