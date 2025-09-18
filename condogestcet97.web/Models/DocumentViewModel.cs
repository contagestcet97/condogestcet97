using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime EmissionDate { get; set; }

        public DocumentType? Type { get; set; }

        [Display(Name = "Meeting")]
        public int? MeetingId { get; set; }
        [Display(Name = "Intervention")]
        public int? InterventionId { get; set; }

        public IEnumerable<SelectListItem>? Meetings { get; set; }
        public IEnumerable<SelectListItem>? Interventions { get; set; }
    }
}
