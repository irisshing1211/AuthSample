using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuthSample.Models;

public class Permission
{
    public Guid Rd { get; set; }

    public string Code { get; set; }
    public string Module { get; set; }
    public string Func { get; set; }
    public string Action { get; set; }
    public ICollection<Role> Roles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}
