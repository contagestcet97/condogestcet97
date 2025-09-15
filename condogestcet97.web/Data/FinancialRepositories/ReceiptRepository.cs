using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        private readonly DataContextFinancial _context;

        public ReceiptRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }
    }
}
