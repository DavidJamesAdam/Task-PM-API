using Microsoft.AspNetCore.Identity;
using Task_manager.Models;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<Users>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        // 1. Create roles
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
            }
        }

        // 2. Create admin user if not exists
        var adminEmail = "admin@test.com";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new Users
            {
                UserName = adminEmail,
                Email = adminEmail,
                Fname = "Admin",
                Lname = "User"
            };

            await userManager.CreateAsync(adminUser, "Password123!");
        }

        // Ensure admin user has Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}