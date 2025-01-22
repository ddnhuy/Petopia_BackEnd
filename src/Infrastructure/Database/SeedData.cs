using Domain.Users;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Infrastructure.Database;
public static class SeedData
{
    public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync(AppRoles.SUPERADMIN).GetAwaiter().GetResult())
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.SUPERADMIN));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.ADMIN));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.USER));
        }

        string userName = "petopia";
        string password = "Petopia123@";

        ApplicationUser? user = await userManager.FindByEmailAsync(userName);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = userName,
                Email = "admin@petopia.com",
                FirstName = "Petopia",
                LastName = "Super Admin",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            IdentityResult result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.ConfirmEmailAsync(user, await userManager.GenerateEmailConfirmationTokenAsync(user));
                await userManager.AddToRoleAsync(user, AppRoles.SUPERADMIN);
            }
        }
    }
}

