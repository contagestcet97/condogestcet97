using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Condo : IEntity
    {
        public int Id { get ; set ; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Address { get; set ; }

        //public ICollection<Apartment> Apartments { get; set ; }
    }
}
