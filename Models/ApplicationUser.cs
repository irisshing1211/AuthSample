using Microsoft.AspNetCore.Identity;

namespace AuthSample.Models;

public class ApplicationUser: IdentityUser
{
    public string Name { get; set; }
}
