using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Models
{
    public class MeetingViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public DateTime Date { get; set; }

        public MeetingType Type { get; set; }

        public int CondoId { get; set; }

        public IEnumerable<SelectListItem>? Condos { get; set; }
    }
}
