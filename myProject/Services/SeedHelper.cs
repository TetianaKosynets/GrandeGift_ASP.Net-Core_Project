using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myProject.Models;
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

                //IDataService<Profile> _dataServiceProf = scope.ServiceProvider.GetRequiredService<IDataService<Profile>>();
                //IDataService<Address> _dataServiceAddress = scope.ServiceProvider.GetRequiredService<IDataService<Address>>();

                //IDataService<Category> _dataServiceCat = scope.ServiceProvider.GetRequiredService<IDataService<Category>>();
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


                    //Profile prof = new Profile
                    //{
                    //    UserID = admin.Id,
                    //    FirstName = " ",
                    //    LastName = " ",
                    //};

                    //Address address = new Address
                    //{
                    //    UserID = admin.Id,
                    //    StreetAddress = " ",
                    //    City = " ",
                    //    State = " ",
                    //    PostCode = " "
                    //};

                    //_dataServiceProf.Create(prof);
                    //_dataServiceAddress.Create(address);
                }
                //if (_dataServiceCat.Exist(c => c.CategoryName == "Christmas Hampers") == false)
                //{
                //    Category cat = new Category
                //    {
                //        CategoryName = "Christmas Hampers"
                //    };

                //    _dataServiceCat.Create(cat);
                //}

                //if (_dataServiceCat.Exist(c => c.CategoryName == "Hampers for Her") == false)
                //{
                //    Category cat = new Category
                //    {
                //        CategoryName = "Hampers for Her"
                //    };

                //    _dataServiceCat.Create(cat);
                //}

                //if (_dataServiceCat.Exist(c => c.CategoryName == "Hampers for Him") == false)
                //{
                //    Category cat = new Category
                //    {
                //        CategoryName = "Hampers for Him"
                //    };

                //    _dataServiceCat.Create(cat);
                //}

                //if (_dataServiceCat.Exist(c => c.CategoryName == "Baby Hampers") == false)
                //{
                //    Category cat = new Category
                //    {
                //        CategoryName = "Baby Hampers"
                //    };

                //    _dataServiceCat.Create(cat);
                //}

                //if (_dataServiceCat.Exist(c => c.CategoryName == "Food Hampers") == false)
                //{
                //    Category cat = new Category
                //    {
                //        CategoryName = "Food Hampers"
                //    };

                //    _dataServiceCat.Create(cat);
                //}

            }
        }
    }
}
