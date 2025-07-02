using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Data.Repositories;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class IncidentRepository : GenericRepository<Incident>, IIncidentRepository
    {

        private readonly DataContextCondominium _context;

        public IncidentRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }
        public override IQueryable<Incident> GetAll()
        {
            return _context.Incidents.Include(i => i.Condo);
        }

        public Task<Incident> GetByIdTrackedAsync(int id)
        {
            return _context.Incidents
                .Include(i => i.Condo)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public override Task<Incident> GetByIdAsync(int id)
        {
            return _context.Incidents
                .Include(i => i.Condo)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
