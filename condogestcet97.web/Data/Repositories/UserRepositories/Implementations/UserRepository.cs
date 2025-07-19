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

    }

}
