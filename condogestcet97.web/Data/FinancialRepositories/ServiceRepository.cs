using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class ServiceRepository : FinancialGenericRepository<Service>, IServiceRepository
    {
        private readonly DataContextFinancial _context;
        public ServiceRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }
    }
}
