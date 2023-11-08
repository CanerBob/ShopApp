using Microsoft.AspNetCore.Identity;

namespace ShopAPP.Identities;
public class Person:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
   
}