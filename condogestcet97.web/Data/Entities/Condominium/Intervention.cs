using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Intervention : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(25)]
        public string Title { get; set; }

        [Column(TypeName = "varchar(500)")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string CompanyName { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public int IncidentId { get; set; }

        public Incident? Incident { get; set; }
    }
}
