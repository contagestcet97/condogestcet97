using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class InterventionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string CompanyName { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public int IncidentId { get; set; }

        public IEnumerable<SelectListItem>? Incidents { get; set; }
    }
}
