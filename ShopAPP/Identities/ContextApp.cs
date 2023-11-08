using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShopAPP.Identities;
public class ContextApp:IdentityDbContext<Person>
{
    public ContextApp(DbContextOptions<ContextApp> options):base(options)
    {
        
    }
}
