using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.FinancialRepositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly DataContextFinancial _context;
        public ExpenseRepository(DataContextFinancial context) : base(context)
        {
            _context = context;
        }

        public override IQueryable<Expense> GetAll()
        {
            return _context.Expenses.Include(a => a.Quota).Include(a => a.Service);
        }
    }
}
