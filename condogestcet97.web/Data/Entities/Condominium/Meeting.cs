using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public enum MeetingType
    {
        [Display(Name = "In person")]
        InPerson,
        Online
    }
    public class Meeting : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(25)")]
        [MaxLength(25)]
        public string Topic { get; set; }

        public DateTime Date { get; set; }

        public MeetingType Type { get; set; }

        public int CondoId { get; set; }
        public Condo? Condo { get; set; }

    }
}
