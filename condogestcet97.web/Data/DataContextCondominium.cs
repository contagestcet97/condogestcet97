using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data
{
    public class DataContextCondominium : DbContext
    {

        public DbSet<Condo> Condos { get; set; }

        public DbSet<Apartment> Apartments { get; set; }

        public DataContextCondominium(DbContextOptions<DataContextCondominium> options) : base(options)
        {

        }
    }
}
