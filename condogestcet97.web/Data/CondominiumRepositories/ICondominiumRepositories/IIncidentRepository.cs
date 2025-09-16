using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories
{
    public interface IIncidentRepository : ICondominiumsGenericRepository<Incident>
    {
        Task<Incident> GetByIdTrackedAsync(int id);
    }
}
