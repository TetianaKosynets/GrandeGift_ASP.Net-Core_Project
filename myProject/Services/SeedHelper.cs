using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Services
{
    public class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //sample data

                //add admin role if doesn't exist
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                //add default admin
                if (await userManager.FindByNameAsync("admin") == null)
                {
                    IdentityUser admin = new IdentityUser("admin@grandegift.com");
                    await userManager.CreateAsync(admin, "Admin111"); //add user to user table
                    await userManager.AddToRoleAsync(admin, "Admin"); // add admin1 to role Admin
                }
            }
        }
    }
}
