using condogestcet97.web.Data.Entities;
using condogestcet97.web.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.Repositories
{
    public class ConodominiumsGenericRepository<T> : ICondominiumsGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContextCondominium _context;

        public ConodominiumsGenericRepository(DataContextCondominium context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }



    }
}
