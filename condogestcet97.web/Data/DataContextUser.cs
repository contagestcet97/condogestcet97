using condogestcet97.web.Data.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace condogestcet97.web.Data
{
    public class DataContextUser : IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public new DbSet<User> Users { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public new DbSet<UserRole> UserRoles { get; set; }
        public new DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Recovery> Recoveries { get; set; }


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

            // User: Primary Key
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Role: Primary Key
            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);
        }
    }
}
