using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myProject.Models;
using myProject.Services;

namespace myProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity<IdentityUser, IdentityRole>
                (
                    config =>
                    {
                        config.User.RequireUniqueEmail = false;
                        config.Password.RequireDigit = true;
                        config.Password.RequiredLength = 6;
                        config.Password.RequireLowercase = true;
                        config.Password.RequireUppercase = true;
                        config.Password.RequireNonAlphanumeric = false;
                    }
                ).AddEntityFrameworkStores<MyDbContext>();
            services.AddDbContext<MyDbContext>();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddScoped<IDataService<Profile>, DataService<Profile>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
