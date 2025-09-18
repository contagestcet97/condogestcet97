using condogestcet97.web.Data.Entities.Financial;

namespace condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories
{
    public interface IInvoiceRepository : IFinancialGenericRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<IncomingInvoice> GetInInvoiceAsync(int id);

        Task<OutgoingInvoice> GetOutInvoiceAsync(int id);
    }
}

