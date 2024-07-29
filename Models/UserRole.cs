using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthSample.Models;

public class UserRole : IdentityUserRole<Guid>
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public Guid RoleId { get; set; }

    public Role Role { get; set; }
}
