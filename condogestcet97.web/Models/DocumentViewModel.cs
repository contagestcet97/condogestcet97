using condogestcet97.web.Data.Entities.Condominium;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime EmissionDate { get; set; }

        public DocumentType Type { get; set; }

        public int? MeetingId { get; set; }

        public int? InterventionId { get; set; }
    }
}
