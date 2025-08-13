using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.Repositories.UserRepositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContextUser _context;

        public UserRepository(DataContextUser context) : base(context)
        {
            _context = context;
        }
        // future specific implementations for UserRepository


        public async Task AssignCompaniesAsync(int userId, List<int> companyIds)
        {
            // Get the user with their current company assignments
            var user = await _context.Users
                .Include(u => u.UserCompanies)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return;

            // Remove all current assignments
            user.UserCompanies.Clear();

            // Add new assignments
            foreach (var companyId in companyIds)
            {
                user.UserCompanies.Add(new UserCompany { UserId = userId, CompanyId = companyId });
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Assigns a list of companies to a user as a manager.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        public async Task AssignManagedCompaniesAsync(int userId, List<int> companyIds)
        {
            var user = await _context.Users
                .Include(u => u.ManagedCompanies)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return;

            user.ManagedCompanies.Clear();

            foreach (var companyId in companyIds)
            {
                user.ManagedCompanies.Add(new UserCompanyManager { UserId = userId, CompanyId = companyId });
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of companies managed by a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Company>> GetManagedCompaniesAsync(int userId)
        {
            return await _context.UserCompanyManagers
                .Where(ucm => ucm.UserId == userId)
                .Select(ucm => ucm.Company)
                .ToListAsync();
        }


    }

}
