using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories
{
    public interface IVoteRepository : ICondominiumsGenericRepository<Vote>
    {
        Task<Vote> GetByIdTrackedAsync(int id);
    }
}
