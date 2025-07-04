using System.Net.Sockets;
using System.Reflection.Emit;
using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data
{
    public class DataContextCondominium : DbContext
    {

        public DbSet<Condo> Condos { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Intervention> Interventions { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DataContextCondominium(DbContextOptions<DataContextCondominium> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
           .HasDiscriminator<string>("DocumentType")
           .HasValue<MeetingDocument>("Meeting")
           .HasValue<InterventionDocument>("Intervention");

            foreach (var foreignKey in modelBuilder.Model
             .GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
                    {
                        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                    }

            base.OnModelCreating(modelBuilder);
        }
    }
}
