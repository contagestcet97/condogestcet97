using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class QuotaRepository : GenericRepository<Quota>, IQuotaRepository
    {
        private readonly DataContextFinancial _context;
        public QuotaRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }
    }
}
