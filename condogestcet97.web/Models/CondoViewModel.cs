using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using condogestcet97.web.Data.Entities.Condominium;

namespace condogestcet97.web.Models
{
    public class CondoViewModel
    {
        public int CondoId { get; set; }

        public string Address { get; set; }
    }
}
