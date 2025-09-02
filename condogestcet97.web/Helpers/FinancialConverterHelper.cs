using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers
{
    public class FinancialConverterHelper : IFinancialConverterHelper
    {
        public Expense ToExpense(ExpenseViewModel model, bool isNew)
        {
            return new Expense
            {
                Id = isNew ? 0 : model.Id,
                Amount = model.Amount,
                Description = model.Description,
                IsFullyPaid = model.IsFullyPaid,
                QuotaId = model.QuotaId,
                ServiceId = model.ServiceId,
            };
        }

        public ExpenseViewModel ToExpenseViewModel(Expense expense)
        {
            return new ExpenseViewModel
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description,
                IsFullyPaid = expense.IsFullyPaid,
                QuotaId = expense.QuotaId,
                ServiceId = expense.ServiceId,
            };
        }

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

        public Service ToService(ServiceViewModel model, bool isNew)
        {
            return new Service
            {
                Id = isNew ? 0 : model.Id,
                CondoId = model.CondoId,
                FinishDate = model.FinishDate,
                DefaultFee = model.DefaultFee,
                Description = model.Description,
                StartDate = model.StartDate,
                CompanyName = model.CompanyName,
                IsRecurring = model.IsRecurring,
            };
        }

        public ServiceViewModel ToServiceViewModel(Service service)
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                CondoId = service.CondoId,
                FinishDate = service.FinishDate,
                DefaultFee = service.DefaultFee,
                Description = service.Description,
                StartDate = service.StartDate,
                CompanyName = service.CompanyName,
                IsRecurring = service.IsRecurring,
            };
        }

        
    }
}
