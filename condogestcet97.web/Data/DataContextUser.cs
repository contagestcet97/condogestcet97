using condogestcet97.web.Data.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace condogestcet97.web.Data
{
    public class DataContextUser : IdentityDbContext<User, Role, int>
    {
        public DbSet<Company> Companies { get; set; } // DbSet for Company entity, representing the companies in the system
        //public DbSet<User> Users { get; set; } // IdentityDbContext already includes Users
        //public DbSet<Role> Roles { get; set; } // IdentityDbContext already includes Roles
        //public DbSet<UserRole> UserRoles { get; set; } // IdentityDbContext already includes UserRoles
        //public DbSet<RoleClaim> RoleClaims { get; set; } // IdentityDbContext already includes RoleClaims
        public DbSet<UserCompany> UserCompanies { get; set; } // DbSet for UserCompany entity, representing the many-to-many relationship between users and companies
        public DbSet<Token> Tokens { get; set; } // DbSet for Token entity, representing authentication tokens for users
        public DbSet<Recovery> Recoveries { get; set; } // DbSet for Recovery entity, representing recovery codes for two-factor authentication


        public DbSet<UserCompanyManager> UserCompanyManagers { get; set; } // DbSet for UserCompanyManager entity, representing the many-to-many relationship between users and companies for managers


        public DataContextUser(DbContextOptions<DataContextUser> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserRole: Composite Key
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // UserCompany: Composite Key
            modelBuilder.Entity<UserCompany>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });

            modelBuilder.Entity<UserCompany>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCompany>()
                .HasOne(uc => uc.Company)
                .WithMany(c => c.UserCompanies)
                .HasForeignKey(uc => uc.CompanyId);

            // RoleClaim: Primary Key
            modelBuilder.Entity<RoleClaim>()
                .HasKey(rc => rc.Id);

            modelBuilder.Entity<RoleClaim>()
                .HasOne(rc => rc.Role)
                .WithMany(r => r.RoleClaims)
                .HasForeignKey(rc => rc.RoleId);

            // Token: Primary Key
            modelBuilder.Entity<Token>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Token>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.UserId);

            // Recovery: Primary Key
            modelBuilder.Entity<Recovery>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recovery>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recoveries)
                .HasForeignKey(r => r.UserId);

            // Company: Primary Key
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            // Role: Primary Key
            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);


            // UserCompanyManager: Composite Key for the many-to-many relationship between User and Company for managers
            modelBuilder.Entity<UserCompanyManager>()
                .HasKey(ucm => new { ucm.UserId, ucm.CompanyId });

            modelBuilder.Entity<UserCompanyManager>()
                .HasOne(ucm => ucm.User)
                .WithMany(u => u.ManagedCompanies)
                .HasForeignKey(ucm => ucm.UserId);

            modelBuilder.Entity<UserCompanyManager>()
                .HasOne(ucm => ucm.Company)
                .WithMany(c => c.Managers)
                .HasForeignKey(ucm => ucm.CompanyId);
        }
    }
}
