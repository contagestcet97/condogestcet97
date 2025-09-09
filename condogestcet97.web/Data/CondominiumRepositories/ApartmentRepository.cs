using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Data.Repositories;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class ApartmentRepository : ConodominiumsGenericRepository<Apartment>, IApartmentRepository
    {
        private readonly DataContextCondominium _context;

        public ApartmentRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Apartment> GetAll()
        {
            return _context.Apartments.Include(a => a.Condo);
        }
        public override Task<Apartment> GetByIdAsync(int id)
        {
            return _context.Apartments
                .Include(a => a.Condo)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public Task<Apartment> GetByIdTrackedAsync(int id)
        {
            return _context.Apartments
                .Include(a => a.Condo)
                .FirstOrDefaultAsync(a => a.Id == id);
        }



    }
}
