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
                };

                _context.Quotas.Add(quota);

            }
            await _context.SaveChangesAsync();
        }
    }
}
