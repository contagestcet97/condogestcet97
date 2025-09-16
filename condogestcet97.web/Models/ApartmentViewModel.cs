using condogestcet97.web.Data.Entities.Condominium;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Models
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }

        public int CondoId { get; set; }


        public string? CondoAddress { get; set; }

        public string Flat { get; set; }

        public string Divisions { get; set; }
    }
}
