using condogestcet97.web.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace condogestcet97.web.Data.Seed
{
    public static class SeedDbUser
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // application's DbContext
            var dbContext = serviceProvider.GetRequiredService<DataContextUser>();

            // RoleManager and UserManager from DI
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await dbContext.Database.EnsureCreatedAsync();

            // seed Roles
            string[] roles = new[] { "Admin", "Employee", "Resident" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            // Seed dummy company if it does not exist
            var dummyCompanyName = "Simao Company";

            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Name == dummyCompanyName);
            if (company == null)
            {
                company = new Company
                {
                    Name = dummyCompanyName,
                    Address = "123 street",
                    Phone = "123456789",
                    FiscalNumber = "123456789"
                };
                dbContext.Companies.Add(company);
                await dbContext.SaveChangesAsync();
            }

            // seed Admin user
            var adminEmail = "spmmazb@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail, // Need to use the email as the username for Identity to login
                    Email = adminEmail,
                    Name = "Admin",
                    EmailConfirmed = true,
                    TwoFAEnabled = false,
                    Address = "Admin Address",
                    PhoneNumber = "999999999",
                    FiscalNumber = "999999999"
                };
                await userManager.CreateAsync(adminUser, "123456Aa!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // seed Employee user
            var employeeEmail = "contagestcet97@gmail.com"; // Change this to the desired employee email
            var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
            if (employeeUser == null)
            {
                employeeUser = new User
                {
                    UserName = employeeEmail, // Need to use the email as the username for Identity to login
                    Email = employeeEmail,
                    Name = "Employee",
                    EmailConfirmed = true,
                    TwoFAEnabled = false,
                    Address = "Employee Address",
                    PhoneNumber = "888888888",
                    FiscalNumber = "888888888"
                };
                await userManager.CreateAsync(employeeUser, "123456Aa!");
                await userManager.AddToRoleAsync(employeeUser, "Employee");
            }

            var residentEmail = "jackjones@yopmail.com"; 
            var residentUser = await userManager.FindByEmailAsync(residentEmail);

            if (residentUser == null)
            {

                residentUser = new User
                {
                    UserName = residentEmail, // Need to use the email as the username for Identity to login
                    Email = residentEmail,
                    Name = "Jack Jones",
                    EmailConfirmed = true,
                    TwoFAEnabled = false,
                    CondoId = 1,
                    ApartmentId = 1,
                    Address = "Rua do Olival 100",
                    PhoneNumber = "888888888",
                    FiscalNumber = "888888888"
                };
                await userManager.CreateAsync(residentUser, "123456Aa!");
                await userManager.AddToRoleAsync(residentUser, "Employee");
            }
        }
    }
}
