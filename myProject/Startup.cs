using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myProject.Models;
using myProject.Services;

namespace myProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(15);
                options.Cookie.HttpOnly = true;
            });

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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
            //services.AddDbContext<MyDbContext>();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddScoped<IDataService<Profile>, DataService<Profile>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Hamper>, DataService<Hamper>>();
            services.AddScoped<IDataService<Cart>, DataService<Cart>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
