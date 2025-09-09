using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories
{
    public interface IMeetingRepository : ICondominiumsGenericRepository<Meeting>
    {
        Task<Meeting> GetByIdTrackedAsync(int id);
    }
}
