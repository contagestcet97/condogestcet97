using condogestcet97.web.Data.Entities.Users;
using System.Collections.Generic;

namespace condogestcet97.web.Data.Repositories.UserRepositories.Interfaces
{
    /// <summary>
    /// Interface for User repository that extends the generic repository.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Assigns a list of companies to a user.
        /// </summary>
        /// <param name="userId"> The ID of the user to whom companies will be assigned.</param>
        /// <param name="companyIds"> A list of company IDs to assign to the user.</param>
        /// <returns></returns>
        Task AssignCompaniesAsync(int userId, List<int> companyIds);

        /// <summary>
        /// Assigns a list of companies to a user as a manager.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        Task AssignManagedCompaniesAsync(int userId, List<int> companyIds);
        Task<List<Company>> GetManagedCompaniesAsync(int userId);

        Task<IEnumerable<User>> GetUsersByCondo(int condoId);
    }
}
