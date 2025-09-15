using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Models;

namespace condogestcet97.web.Helpers.IHelpers
{
    public interface IFinancialConverterHelper
    {
        Quota ToQuota(QuotaViewModel model, bool isNew);

        QuotaViewModel ToQuotaViewModel(Quota quota);

        Service ToService(ServiceViewModel model, bool isNew);

        ServiceViewModel ToServiceViewModel(Service service);

        Expense ToExpense(ExpenseViewModel model, bool isNew);

        ExpenseViewModel ToExpenseViewModel(Expense expense);

        Invoice ToInvoice(InvoiceViewModel model, bool isNew);

        InvoiceViewModel ToInvoiceViewModelFromIncomingInvoice(IncomingInvoice invoice);

        InvoiceViewModel ToInvoiceViewModelFromOutgoingInvoice(OutgoingInvoice invoice);

        Payment ToPayment(PaymentViewModel model, bool isNew);

        PaymentViewModel ToPaymentViewModel(Payment payment);

        Receipt ToReceipt(ReceiptViewModel model, bool isNew);

        ReceiptViewModel ToReceiptViewModel(Receipt receipt);


    }
}
