using System.Linq;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Data.CondominiumRepositories
{
    public class DocumentRepository : ConodominiumsGenericRepository<Document>, IDocumentRepository
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
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<InterventionDocument> GetInterventionDocAsync(int id)
        {
            return await _context.Documents
                .OfType<InterventionDocument>()
                .AsNoTracking().               
                Include(d => d.Intervention)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<MeetingDocument> GetMeetDocTrackedAsync(int id)
        {
            return await _context.Documents
                .OfType<MeetingDocument>()
                .Include(d => d.Meeting)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<InterventionDocument> GetInterventionDocTrackedAsync(int id)
        {
            return await _context.Documents
                .OfType<InterventionDocument>()
                .Include(d => d.Intervention)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var meetingDocuments = (await _context.Documents
                .OfType<MeetingDocument>()
                .Include(d => d.Meeting)
                .AsNoTracking()
                .ToListAsync())
                .Cast<Document>();

            var interventionDocuments = (await _context.Documents
                .OfType<InterventionDocument>()
                .Include(d => d.Intervention)
                .AsNoTracking()
                .ToListAsync())
                .Cast<Document>();

            return meetingDocuments.Concat(interventionDocuments);
        }


        public async Task<IEnumerable<Document>> GetAllTrackedAsync()
        {
            var meetingDocuments = (await _context.Documents
                .OfType<MeetingDocument>()
                .Include(d => d.Meeting)
                .ToListAsync())
                .Cast<Document>();

            var interventionDocuments = (await _context.Documents
                .OfType<InterventionDocument>()
                .Include(d => d.Intervention)
                .ToListAsync())
                .Cast<Document>();

            return meetingDocuments.Concat(interventionDocuments);
        }



    }
}
