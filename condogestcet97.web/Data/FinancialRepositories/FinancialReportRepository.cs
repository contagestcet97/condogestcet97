using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class FinancialReportRepository : GenericRepository<FinancialReport>, IFinancialReportRepository
    {
        private readonly DataContextFinancial _context;
        public FinancialReportRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }
    }
}
