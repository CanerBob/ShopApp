using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopAPP.Identities;
public class RoleModel
{
    [Required]
    public string Name { get; set; }
}
public class RoleDetails 
{
    public IdentityRole Role { get; set; }
    public IEnumerable<Person> Members { get; set; }
    public IEnumerable<Person> NonMembers { get; set; }
}
public class RoleEditModel 
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public string[] IdsToAdd { get; set; }
    public string[] IdsToDelete { get; set; }

}