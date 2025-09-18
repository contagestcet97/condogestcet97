using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.Repositories
{
   
    public class CondoRepository : ConodominiumsGenericRepository<Condo>, ICondoRepository
    {
        private readonly DataContextCondominium _context;

        public CondoRepository(DataContextCondominium context) : base(context)
        {
            _context = context;
        }

        public override Task<Condo> GetByIdAsync(int id)
        {
            return _context.Condos
             .Include(c => c.Apartments)
             .Include(c => c.Meetings)
             .Include(c => c.Incidents)
             .AsNoTracking()
             .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Condo> GetByIdTrackedAsync(int id)
        {
            return _context.Condos
             //.Include(c => c.Apartments)
             .FirstOrDefaultAsync(i => i.Id == id);
        }

        public IEnumerable<SelectListItem> GetComboCondos()
        {
            var list = _context.Condos.Select(a => new SelectListItem
            {
                Text = a.Address,
                Value = a.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a condo...)",
                Value = "0"
            });

            return list;
        }
    }
}
