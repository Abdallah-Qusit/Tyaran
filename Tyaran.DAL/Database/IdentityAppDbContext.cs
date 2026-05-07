using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class IdentityAppDbContext
    : IdentityDbContext<ApplicationUser>
{
    public IdentityAppDbContext(
        DbContextOptions<IdentityAppDbContext> options)
        : base(options)
    {
    }
}