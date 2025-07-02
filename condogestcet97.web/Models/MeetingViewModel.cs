using condogestcet97.web.Data.Entities.Condominium;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Models
{
    public class MeetingViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public DateTime Date { get; set; }

        public MeetingType Type { get; set; }

        public int CondoId { get; set; }
    }
}
