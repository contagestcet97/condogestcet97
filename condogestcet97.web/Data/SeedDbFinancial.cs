using condogestcet97.web.Data.Entities.Financial;
using System.Runtime.InteropServices;

namespace condogestcet97.web.Data
{
    public class SeedDbFinancial
    {

        private readonly DataContextFinancial _context;

        public SeedDbFinancial(DataContextFinancial context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Quotas.Any())
            {
                Quota quota = new Quota
                {
                    DueDate = DateTime.Now.AddDays(10),
                    PaidDate = null,
                    PaymentValue = 24.99m,
                    IsPaid = false,
                    LateFee = 2.00m,
                    CondoId = 1,
                };

                _context.Quotas.Add(quota);

            }

            await _context.SaveChangesAsync();

            if (!_context.Services.Any())
            {
                Service service = new Service
                {
                    Description = "Painting of building facade",
                    IsRecurring = false,
                    StartDate = DateTime.Now.AddDays(-20),
                    FinishDate = DateTime.Now.AddDays(20),
                    DefaultFee = 2000.00m,
                    CompanyName = "LisbonPainters",
                    CondoId = 1,
                    
                };

                Service service2 = new Service
                {
                    Description = "Cleaning of common areas",
                    IsRecurring = true,
                    StartDate = DateTime.Now.AddDays(-360),
                    FinishDate = null,
                    DefaultFee = 40.00m,
                    CompanyName = "WeClean!",
                    CondoId = 1,

                };

                _context.Services.Add(service);
                _context.Services.Add(service2);

            }
            await _context.SaveChangesAsync();

            if (!_context.Expenses.Any())
            {
                var quota = _context.Quotas.FirstOrDefault(i => i.Id == 1);
                var service = _context.Services.FirstOrDefault(i => i.Id == 1);
                var service2 = _context.Services.FirstOrDefault(i => i.Id == 2);

                if (quota != null && service != null && service2 != null)
                {
                    Expense expense = new Expense
                    {
                        Description = "Painting",
                        Amount = 2000.00m,
                        QuotaId = quota.Id,
                        ServiceId = service.Id,
                        IsFullyPaid = false,
                    };

                    Expense expense2 = new Expense
                    {
                        Description = "August Cleaning",
                        Amount = 40.00m,
                        QuotaId = quota.Id,
                        ServiceId = service2.Id,
                        IsFullyPaid = true,
                    };

                    Expense expense3 = new Expense
                    {
                        Description = "Fire and flood insurance 2025-26",
                        Amount = 120.00m,
                        QuotaId = quota.Id,
                        ServiceId = null,
                        IsFullyPaid = false,
                    };

                    _context.Expenses.Add(expense);
                    _context.Expenses.Add(expense2);
                    _context.Expenses.Add(expense3);

                }
            }
            await _context.SaveChangesAsync();

            if (!_context.Invoices.Any())
            {
                var expense = _context.Expenses.FirstOrDefault(i => i.Id == 1);
                var quota = _context.Quotas.FirstOrDefault(i => i.Id == 1);

                if (expense != null)
                {
                    IncomingInvoice invoice = new IncomingInvoice
                    {
                        Description = "Fire and Flood annual insurance",
                        DueDate = DateTime.Now.AddDays(15),
                        IsPaid = true,
                        SupplierContact = "lisboa@seguros.pt",
                        TotalAmount = 120.00m,
                        ExpenseId = expense.Id,
                        SupplierName = "Seguros Portugal",
                        Type = InvoiceType.Incoming

                    };

                    _context.Invoices.Add(invoice);

                    OutgoingInvoice outgoingInvoice = new OutgoingInvoice
                    {
                        Description = "Quota payment August",
                        DueDate = DateTime.Now.AddDays(4),
                        IsPaid = true,
                        TotalAmount = 40.00m,
                        UserId = "Condo user1",
                        QuotaId = quota.Id,
                        EmissionDate = DateTime.Now.AddDays(-10),
                        Type = InvoiceType.Outgoing
                    };

                    _context.Invoices.Add(invoice);
                    _context.Invoices.Add(outgoingInvoice);

                }
            }
            await _context.SaveChangesAsync();

            if (!_context.Payments.Any())
            {
                var invoice = _context.Invoices.FirstOrDefault(i => i.Id == 1);
                var invoice2 = _context.Invoices.FirstOrDefault(i => i.Id == 2);

                if (invoice != null && invoice2 != null)
                {
                    Payment payment = new Payment
                    {
                        PaidDate = DateTime.Now.AddDays(-1),
                        Amount = 120.00m,
                        InvoiceId = invoice.Id,
                        Method = "Visa",
                        UserId = "CompanyName",

                    };

                    Payment payment2 = new Payment
                    {
                        PaidDate = DateTime.Now.AddDays(-5),
                        Amount = 40.00m,
                        InvoiceId = invoice2.Id,
                        Method = "Paypal",
                        UserId = "Condo user1",

                    };

                    _context.Payments.Add(payment);
                    _context.Payments.Add(payment2);
                }

                await _context.SaveChangesAsync();
            }

            if (!_context.Receipts.Any())
            {
                var payment = _context.Payments.FirstOrDefault(i => i.Id == 2);

                if (payment != null) 
                {
                    Receipt receipt = new Receipt
                    {
                        FiscalNumber = "345123678",
                        PayeeName = "Carlos Moedas",
                        PaymentId = payment.Id,
                        Type = ReceiptType.Outgoing,
                    };

                    _context.Receipts.Add(receipt);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
