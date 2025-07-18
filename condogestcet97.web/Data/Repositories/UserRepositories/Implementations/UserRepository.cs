using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;

namespace condogestcet97.web.Data.Repositories.UserRepositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContextUser context) : base(context)
        {
            // future specific implementations for UserRepository
        }
    }

}
