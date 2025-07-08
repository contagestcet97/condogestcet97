using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DataContextCondominium _context;

        public VoteRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Vote> GetAll()
        {
            return _context.Votes.Include(i => i.Meeting);
        }

        public override Task<Vote> GetByIdAsync(int id)
        {
            return _context.Votes.AsNoTracking()
                           .Include(a => a.Meeting)
                           .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Vote> GetByIdTrackedAsync(int id)
        {
            return _context.Votes
                           .Include(a => a.Meeting)
                           .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
