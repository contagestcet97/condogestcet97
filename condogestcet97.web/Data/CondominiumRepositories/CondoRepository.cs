using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.Repositories
{
   
    public class CondoRepository : GenericRepository<Condo>, ICondoRepository
    {
        private readonly DataContextCondominium _context;

        public CondoRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }

        public Task<Condo> GetByIdTrackedAsync(int id)
        {
            return _context.Condos
             .Include(c => c.Apartments)
             .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
