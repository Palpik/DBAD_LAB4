using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Initializer;
public class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
    {
        await applicationDbContext.Database.MigrateAsync();

        string adminEmail = "admin@gmail.com";
        string password = "Pass1_";
        if (await roleManager.FindByNameAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }
        if (await roleManager.FindByNameAsync("employee") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("employee"));
        }
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
                var code = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, code);
            }
        }
    }
}