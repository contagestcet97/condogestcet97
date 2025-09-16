using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Apartment : IEntity
    {
        public int Id { get ; set ; }

        public Condo? Condo { get; set ; }

        public int CondoId { get; set ; }

        [Required]
        [Column(TypeName = "varchar(4)")]
        [MaxLength(4)]
        public string Flat { get; set ; }

        [Required]
        [Column(TypeName = "varchar(3)")]
        [MaxLength(3)]
        public string Divisions { get; set ; }
    }
}
