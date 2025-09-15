using condogestcet97.web.Data;
using condogestcet97.web.Data.FinancialRepositories;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
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

            builder.Services.AddTransient<SeedDbFinancial>();
            builder.Services.AddScoped<IQuotaRepository, QuotaRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
            builder.Services.AddScoped<IFinancialReportRepository, FinancialReportRepository>();
            builder.Services.AddScoped<IFinancialConverterHelper, FinancialConverterHelper>();
            builder.Services.AddDbContext<DataContextFinancial>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FinancialConnection"));
            });




            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<SeedDbFinancial>();
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
