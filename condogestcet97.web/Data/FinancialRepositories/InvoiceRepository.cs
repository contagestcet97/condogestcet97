using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using condogestcet97.web.Migrations;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly DataContextFinancial _context;
        public InvoiceRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var incomingInvoices = (await _context.Invoices
                .OfType<IncomingInvoice>()
                .Include(d => d.Expense)
                .ThenInclude(d => d.Quota)
                //.ThenInclude(d => d.Condo)
                .AsNoTracking()
                .ToListAsync())
                .Cast<Invoice>();

            var outgoingInvoices = (await _context.Invoices
                .OfType<OutgoingInvoice>()
                .Include(d => d.Quota)
                .AsNoTracking()
                .ToListAsync())
                .Cast<Invoice>();

            return incomingInvoices.Concat(outgoingInvoices);
        }

        public async Task<IncomingInvoice> GetInInvoiceAsync(int id)
        {
            return await _context.Invoices
                .OfType<IncomingInvoice>()
                .AsNoTracking().
                Include(d => d.Expense)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<OutgoingInvoice> GetOutInvoiceAsync(int id)
        {
            return await _context.Invoices
                .OfType<OutgoingInvoice>()
                .Include(d => d.Quota)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }

}
