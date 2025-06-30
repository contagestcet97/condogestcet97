using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Incident : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string NotifierId { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(25)]
        public string Title { get; set; }

        [Column(TypeName = "varchar(500)")]
        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsResolved { get; set; }

        public Apartment? Apartment { get; set; }

        public Condo Condo { get; set; }
    }
}
