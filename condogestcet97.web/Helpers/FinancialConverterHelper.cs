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

        public Invoice ToInvoice(InvoiceViewModel model, bool isNew)
        {
            if (model.InvoiceType == InvoiceType.Incoming)
            {
                return new IncomingInvoice
                {
                    Id = isNew ? 0 : model.Id,
                    SupplierContact = model.SupplierContact,
                    SupplierName = model.SupplierName,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    ExpenseId = model.ExpenseId.Value,
                    IsPaid = model.IsPaid,
                    TotalAmount = model.TotalAmount,
                };
            }

            return new OutgoingInvoice
            {
                Id = model.Id,
                Description = model.Description,
                DueDate = model.DueDate,
                EmissionDate = model.EmissionDate.Value,
                IsPaid = model.IsPaid,
                QuotaId = model.QuotaId,
                TotalAmount = model.TotalAmount,
                UserId = model.UserId,

            };
        }

        public InvoiceViewModel ToInvoiceViewModelFromIncomingInvoice(IncomingInvoice invoice)
        {
            return new InvoiceViewModel
            {
                Id = invoice.Id,
                Description =invoice.Description,
                DueDate=invoice.DueDate,
                ExpenseId = invoice.ExpenseId,
                InvoiceType = InvoiceType.Incoming,
                SupplierContact = invoice.SupplierContact,
                SupplierName = invoice.SupplierName,
                IsPaid =invoice.IsPaid,
                TotalAmount =invoice.TotalAmount,
            };
        }

        public InvoiceViewModel ToInvoiceViewModelFromOutgoingInvoice(OutgoingInvoice invoice)
        {
            return new InvoiceViewModel
            {
                Id = invoice.Id,
                Description = invoice.Description,
                DueDate = invoice.DueDate,
                QuotaId = invoice.QuotaId,
                UserId = invoice.UserId,
                EmissionDate = invoice.EmissionDate,
                InvoiceType = InvoiceType.Outgoing,
                IsPaid = invoice.IsPaid,
                TotalAmount = invoice.TotalAmount,
            };
        }

        public Payment ToPayment(PaymentViewModel model, bool isNew)
        {
            return new Payment
            {
                Id = isNew ? 0 : model.Id,
                PaidDate = model.PaidDate,
                Amount = model.Amount,
                InvoiceId = model.InvoiceId,
                Method = model.Method,
                UserId = model.UserId,
            };
        }

        public PaymentViewModel ToPaymentViewModel(Payment payment)
        {
            return new PaymentViewModel
            {
                Id = payment.Id,
                PaidDate = payment.PaidDate,
                Amount = payment.Amount,
                InvoiceId = payment.InvoiceId,
                Method = payment.Method,
                UserId = payment.UserId,
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

        public Receipt ToReceipt(ReceiptViewModel model, bool isNew)
        {
            return new Receipt
            {
                Id = isNew ? 0 : model.Id,
                FiscalNumber = model.FiscalNumber,
                PayeeName = model.PayeeName,
                PaymentId = model.PaymentId,
                Type = model.Type
            };
        }

        public ReceiptViewModel ToReceiptViewModel(Receipt receipt)
        {
            return new ReceiptViewModel
            {
                Id = receipt.Id,
                FiscalNumber = receipt.FiscalNumber,
                PayeeName = receipt.PayeeName,
                PaymentId = receipt.PaymentId,
                Type = receipt.Type
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
