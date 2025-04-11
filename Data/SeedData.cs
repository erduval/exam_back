using Examen.Models;
using Microsoft.AspNetCore.Identity;

namespace Examen.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAsync (RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[] { "Admin", "Client", "Technicien" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedUsersAsync (UserManager<AppUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@maintenance.com");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin@maintenance.com",
                    Email = "admin@maintenance.com",
                    FullName = "Admin User"
                };

                var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var techUser = await userManager.FindByEmailAsync("tech@mail.com");
            if (techUser == null)
            {
                techUser = new AppUser
                {
                    UserName = "tech@mail.com",
                    Email = "tech@mail.com",
                    FullName = "Technicien User"
                };

                var result = await userManager.CreateAsync(techUser, "TechPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(techUser, "Technicien");
                }
            }

            var clientUser = await userManager.FindByEmailAsync("client@mail.com");
            if (clientUser == null)
            {
                clientUser = new AppUser
                {
                    UserName = "client@mail.com",
                    Email = "client@mail.com",
                    FullName = "Client User"
                };

                var result = await userManager.CreateAsync(clientUser, "ClienthPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(clientUser, "Client");
                }
            }
        }
    }
}

