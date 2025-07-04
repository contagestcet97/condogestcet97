using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class InterventionRepository : GenericRepository<Intervention>, IInterventionRepository
    {

        private readonly DataContextCondominium _context;

        public InterventionRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }
        public override IQueryable<Intervention> GetAll()
        {
            return _context.Interventions.Include(i => i.Incident);
        }

        public override Task<Intervention> GetByIdAsync(int id)
        {
            return _context.Interventions
                .Include(i => i.Incident)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Intervention> GetByIdTrackedAsync(int id)
        {
            return _context.Interventions
                .Include(i => i.Incident)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
