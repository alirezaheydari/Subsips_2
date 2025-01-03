using Microsoft.AspNetCore.Identity;

namespace Subsips_2;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an admin user
        var adminUser = new IdentityUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com"
        };

        string adminPassword = "Admin@1234";
        var _user = await userManager.FindByEmailAsync("admin@admin.com");

        if (_user == null)
        {
            var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdminUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
