using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Implementations;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
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

            // Register the generic repository for dependency injection for basic CRUD operations
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // register company repository for company-specific operations
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

            // register user repository for user-specific operations
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            //register role repository for role-specific operations
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            // register UserManager and RoleManager
            builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContextUser>()
                .AddDefaultTokenProviders();

            // Configure AutoMapper to map between ViewModels and Entities
            builder.Services.AddAutoMapper(typeof(UserProfile));

            // Register EmailSettings from appsettings.json
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            // Register email service for DI
            builder.Services.AddScoped<IEmailServices, EmailServices>();

            // asp net core Identity's IEmailSender (for confirmation, etc...):
            builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailServices>();

            // This redirects the user to the login page if they are not authenticated and indicates the correct path for access denied
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            // build is used to create the application instance after services are configured
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

            // Middleware to handle 404 errors and redirect to a custom NotFoundPage
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 404)
                {
                    // Asynchronously clear the response and redirect
                    context.HttpContext.Response.Clear();
                    context.HttpContext.Response.Redirect("/Home/NotFoundPage");
                    await context.HttpContext.Response.CompleteAsync();
                }
            });

            //Endpoints for controllers
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Endpoints for Razor Pages
            app.MapRazorPages();

            app.Run();

            #endregion
        }

    }
}
