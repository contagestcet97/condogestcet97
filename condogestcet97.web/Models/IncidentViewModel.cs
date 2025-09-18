using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class IncidentViewModel 
    {
        public int Id { get; set; }

        [Display(Name = "Notifier Id")]
        public int NotifierId { get; set; }

        public string Title { get; set; }


        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Resolved")]
        public bool IsResolved { get; set; }

        public int? ApartmentId { get; set; }

        [Display(Name = "Flat")]
        public string? ApartmentFlat {  get; set; }

        public int CondoId { get; set; }

        [Display(Name = "Address")]
        public string? CondoAddress { get; set; }

        public IEnumerable<SelectListItem>? Condos { get; set; }
        public IEnumerable<SelectListItem>? Apartments { get; set; }
        public IEnumerable<SelectListItem>? Users { get; set; }
    }
}
