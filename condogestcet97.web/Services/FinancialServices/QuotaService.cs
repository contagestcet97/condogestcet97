
using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Services.FinancialServices
{

    public class QuotaService : IQuotaService
    {
        private readonly IQuotaRepository _quotaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public QuotaService(IQuotaRepository quotaRepository, IUserRepository userRepository,  IInvoiceRepository invoiceRepository)
        {
            _quotaRepository = quotaRepository;
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
        }


        public async Task CreateQuotaInvoices(Quota quota)
        {

           var users = await _userRepository.GetUsersByCondo(quota.CondoId);

            foreach (var user in users) 
            {
                var outgoingInvoice = new OutgoingInvoice
                {
                    QuotaId = quota.Id,
                    EmissionDate = DateTime.Now,
                    Description = "Quota payment",
                    DueDate = quota.DueDate,
                    IsPaid = false,
                    TotalAmount = quota.PaymentValue,
                    UserId = user.Id,
                };

                await _invoiceRepository.CreateAsync(outgoingInvoice);
            }

            quota.InvoicesSent = true;

        }
    }
}
