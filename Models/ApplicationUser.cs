using Microsoft.AspNetCore.Identity;

namespace AuthSample.Models;

public class ApplicationUser: IdentityUser<Guid>
{
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Role> Roles { get; set; }
}
