using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.WebUI.Entities;

public class CustomIdentityDbContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole, string>
{
    public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> opt)
        :base(opt) { }
}
