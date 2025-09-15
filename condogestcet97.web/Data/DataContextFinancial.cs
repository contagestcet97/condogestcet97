using condogestcet97.web.Data.Entities.Financial;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace condogestcet97.web.Data
{
    public class DataContextFinancial : DbContext
    {
        public DbSet<Quota> Quotas { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DataContextFinancial(DbContextOptions<DataContextFinancial> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quota>()
                .Property(p => p.LateFee)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Quota>()
               .Property(p => p.PaymentValue)
               .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Expense>()
               .Property(p => p.Amount)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Service>()
           .Property(p => p.DefaultFee)
           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
           .Property(p => p.Amount)
           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Invoice>()
            .Property(p => p.TotalAmount)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Invoice>()
           .HasDiscriminator<string>("InvoiceType")
           .HasValue<IncomingInvoice>("Incoming")
           .HasValue<OutgoingInvoice>("Outgoing");

            foreach (var foreignKey in modelBuilder.Model
             .GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
