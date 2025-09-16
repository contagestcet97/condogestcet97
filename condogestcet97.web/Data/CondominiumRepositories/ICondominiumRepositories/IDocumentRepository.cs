using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories
{
    public interface IDocumentRepository : ICondominiumsGenericRepository<Document>
    {

        Task<MeetingDocument> GetMeetDocAsync(int id);

        Task<InterventionDocument> GetInterventionDocAsync(int id);

        Task<MeetingDocument> GetMeetDocTrackedAsync(int id);

        Task<InterventionDocument> GetInterventionDocTrackedAsync(int id);

        Task<IEnumerable<Document>> GetAllTrackedAsync();

        Task<IEnumerable<Document>> GetAllAsync();
    }
}
