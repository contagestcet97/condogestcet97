using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly DataContextFinancial _context;
        public PaymentRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Payment> GetAll()
        {
            return _context.Payments.Include(p => p.Invoice);
        }

        public override async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
