using condogestcet97.web.Data.Entities.Financial;

namespace condogestcet97.web.Services.FinancialServices
{
    public interface IQuotaService
    {
        Task CreateQuotaInvoices(Quota quota);
    }
}
