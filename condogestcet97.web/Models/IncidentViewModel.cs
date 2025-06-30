using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using condogestcet97.web.Data.Entities.Condominium;

namespace condogestcet97.web.Models
{
    public class IncidentViewModel 
    {
        public int Id { get; set; }

        public string NotifierId { get; set; }

        public string Title { get; set; }


        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsResolved { get; set; }

        public int? ApartmentId { get; set; }

        public string? ApartmentFlat {  get; set; }

        public int CondoId { get; set; }

        public string CondoAddress { get; set; }
    }
}
