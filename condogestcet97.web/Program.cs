using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Seed;
using condogestcet97.web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services Configuration

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Razor Pages support
            builder.Services.AddRazorPages();

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<DataContextUser>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // register UserManager and RoleManager
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContextUser>()
                .AddDefaultTokenProviders();


            // Configure AutoMapper to map between ViewModels and Entities
            builder.Services.AddAutoMapper(typeof(UserProfile));

            var app = builder.Build();

            #endregion


            // Seed the database with initial data
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedDbUser.SeedAsync(services);
            }

            #region Middleware Configuration

            // Configure the HTTP request pipeline middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication(); // adding authentication middleware before authorization middleware
            app.UseAuthorization();

            #endregion


            #region Endpoints Configuration

            //Endpoints for controllers
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            #endregion
        }
    }
}
