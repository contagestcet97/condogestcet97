using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{

    public class MeetingRepository : ConodominiumsGenericRepository<Meeting>, IMeetingRepository
    {
        private readonly DataContextCondominium _context;

        public MeetingRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Meeting> GetAll()
        {
            return _context.Meetings.Include(i => i.Condo);
        }

        public override Task<Meeting> GetByIdAsync(int id)
        {
            return _context.Meetings
                .Include(a => a.Condo)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Meeting> GetByIdTrackedAsync(int id)
        {
            return _context.Meetings
                .Include(i => i.Condo)
                .FirstOrDefaultAsync(i => i.Id == id);
        }



    }
}
