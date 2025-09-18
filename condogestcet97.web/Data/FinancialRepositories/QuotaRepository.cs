using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class QuotaRepository : FinancialGenericRepository<Quota>, IQuotaRepository
    {
        private readonly DataContextFinancial _context;
        public QuotaRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Quota> GetAll()
        {
            return _context.Quotas.Include(p => p.Expenses);
        }

        public override async Task<Quota> GetByIdAsync(int id)
        {
            return await _context.Quotas
                .Include(p => p.Expenses)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
