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

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string CompanyName { get; set; }

        public string CompanyTelephone { get; set; }

        public string CompanyEmail { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public Incident Incident { get; set; }
    }
}
