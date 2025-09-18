using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories
{
    public interface IApartmentRepository : ICondominiumsGenericRepository<Apartment>
    {
        Task<Apartment> GetByIdTrackedAsync(int id);

        Task<int> GetApartmentsCount(int condoId);

    }
}
