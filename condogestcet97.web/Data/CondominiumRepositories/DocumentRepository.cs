using System.Linq;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly DataContextCondominium _context;

        public DocumentRepository(DataContextCondominium context) : base(context) 
        {
            _context = context;
        }

        public async Task<MeetingDocument> GetMeetDocAsync(int id)
        {
            return await _context.Documents
                .OfType<MeetingDocument>()
                .AsNoTracking()
                .Include(d => d.Meeting)
                .ThenInclude(m => m.Condo)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<InterventionDocument> GetInterventionDocAsync(int id)
        {
            return await _context.Documents
                .OfType<InterventionDocument>()
                .AsNoTracking().               Include(d => d.Intervention)
                    .ThenInclude(i => i.Incident)
                        .ThenInclude(inc => inc.Condo)
                .Include(d => d.Intervention)
                    .ThenInclude(i => i.Incident)
                        .ThenInclude(inc => inc.Apartment)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<MeetingDocument> GetMeetDocTrackedAsync(int id)
        {
            return await _context.Documents
                .OfType<MeetingDocument>()
                .Include(d => d.Meeting)
                .ThenInclude(m => m.Condo)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<InterventionDocument> GetInterventionDocTrackedAsync(int id)
        {
            return await _context.Documents
                .OfType<InterventionDocument>()
                .Include(d => d.Intervention)
                    .ThenInclude(i => i.Incident)
                        .ThenInclude(inc => inc.Condo)
                .Include(d => d.Intervention)
                    .ThenInclude(i => i.Incident)
                        .ThenInclude(inc => inc.Apartment)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        //public async Task<List<Document>> GetAllTrackedAsync()
        //{
        //    var meetingDocuments = await _context.Documents
        //        .OfType<MeetingDocument>()
        //        .Include(d => d.Meeting)
        //        .ToListAsync();

        //    var interventionDocuments = await _context.Documents
        //        .OfType<InterventionDocument>()
        //        .Include(d => d.Intervention)
        //        .ToListAsync();

        //    return meetingDocuments.Cast<Document>()
        //        .Concat(interventionDocuments.Cast<Document>())
        //        .ToList();
        //}


        public async Task<IEnumerable<Document>> GetAllTrackedAsync()
        {
            var meetingDocuments = (await _context.Documents
                .OfType<MeetingDocument>()
                .Include(d => d.Meeting)
                .ThenInclude(m => m.Condo)
                .ToListAsync())
                .Cast<Document>();

            var interventionDocuments = (await _context.Documents
                .OfType<InterventionDocument>()
                .Include(d => d.Intervention)
                .ThenInclude(m => m.Incident)
                .ThenInclude(m => m.Condo)
                .ToListAsync())
                .Cast<Document>();

            return meetingDocuments.Concat(interventionDocuments);
        }



    }
}
