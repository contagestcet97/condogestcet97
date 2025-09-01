using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace condogestcet97.web.Data.Repositories.IRepositories
{
    public interface ICondoRepository : IGenericRepository<Condo>
    {
        Task<Condo> GetByIdTrackedAsync(int id);

        IEnumerable<SelectListItem> GetComboCondos();
    }
}
