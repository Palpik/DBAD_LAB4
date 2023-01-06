using Microsoft.AspNetCore.Identity;
using WebApp.Data;
using WebApp.Initializer;

namespace WebApp.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider, AdAgencyContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {

            await RoleInitializer.InitializeAsync(userManager, roleManager, applicationDbContext);

            await _next.Invoke(context);
        }
    }
}
