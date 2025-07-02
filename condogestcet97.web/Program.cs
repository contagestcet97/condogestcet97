using condogestcet97.web.Data;
using condogestcet97.web.Data.CondominiumRepositories;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Repositories;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<SeedDbCondominium>();
            builder.Services.AddScoped<ICondoRepository, CondoRepository>();
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
            builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
            builder.Services.AddScoped<IInterventionRepository, InterventionRepository>();
            builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
            builder.Services.AddScoped<ICondiminiumsConverterHelper, CondominiumsConverterHelper>();
  

            builder.Services.AddDbContext<DataContextCondominium>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CondominiumConnection"));
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<SeedDbCondominium>();
                await seeder.SeedAsync();
            }

            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
