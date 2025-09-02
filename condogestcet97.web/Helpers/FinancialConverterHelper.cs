using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers
{
    public class FinancialConverterHelper : IFinancialConverterHelper
    {
        public Quota ToQuota(QuotaViewModel model, bool isNew)
        {
            return new Quota
            {
                Id = isNew ? 0 : model.Id,
                CondoId = model.CondoId,
                PaidDate = model.PaidDate,
                PaymentValue = model.PaymentValue,
                LateFee = model.LateFee,
                IsPaid = model.IsPaid,
                DueDate = model.DueDate
            };
        }

        public QuotaViewModel ToQuotaViewModel(Quota quota)
        {
            return new QuotaViewModel
            {
                Id = quota.Id,
                CondoId = quota.CondoId,
                PaidDate = quota.PaidDate,
                PaymentValue = quota.PaymentValue,
                LateFee = quota.LateFee,
                IsPaid = quota.IsPaid,
                DueDate = quota.DueDate
            };
        }
    }
}
