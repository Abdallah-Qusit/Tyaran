using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;
namespace Tyaran.BLL.Helpers
{

    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider services)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
            "Admin",
            "User",
            "Delivery",
            "RestaurantOwner"
        };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var db = services.GetRequiredService<TyaranDbContext>();

            const string adminEmail = "admin@tyaran.com";
            const string adminPassword = "Admin@123";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Admin"
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (!result.Succeeded)
                    throw new Exception("Admin creation failed");

                await userManager.AddToRoleAsync(admin, "Admin");
            }

            var profileExists = await db.Users
                .AnyAsync(u => u.IdentityUserId == admin.Id);

            if (!profileExists)
            {
                db.Users.Add(new User
                {
                    IdentityUserId = admin.Id,
                    CreatedAt = DateTime.UtcNow,
                    InActive = false
                });

                await db.SaveChangesAsync();
            }
        }


    }
}
