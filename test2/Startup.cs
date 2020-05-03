using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using test2.Services;
using test2.Helpers;

namespace test2
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<PerformanceDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPerformanceService, PerformanceService>();
            services.AddScoped<IPerformanceHelper, PerformanceHelper>();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(config =>
                            new ApiResourcesClaimsService(config).Build());

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                    spa.UseAngularCliServer(npmScript: "start");
            });
            //await InitRoles(app.ApplicationServices); //разкомментировать для добавления роли админа
        }

        // Добавление роли админа пользователю
        private async Task InitRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager;
            UserManager<ApplicationUser> userManager;
            using (var scope = serviceProvider.CreateScope())
            {
                userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
                roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
                IdentityResult roleResult;

                var adminRoleCheck = await roleManager.RoleExistsAsync("Admin");

                if (!adminRoleCheck)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                ApplicationUser user = await userManager.FindByEmailAsync("admin@gmail.com"); // подставляем логин (email) пользователя
                IdentityResult roleAddCheck = await userManager.AddToRoleAsync(user, "Admin");
            };
        }
    }
}
