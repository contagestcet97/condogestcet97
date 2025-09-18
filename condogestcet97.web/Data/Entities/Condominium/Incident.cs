using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Incident : IEntity
    {
        [Display(Name = "Incident ID")]
        public int Id { get; set; }

        public int NotifierId { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(25)]
        public string Title { get; set; }

        [Column(TypeName = "varchar(500)")]
        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsResolved { get; set; }

        public int? ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }

        public int CondoId { get; set; }   
        public Condo? Condo { get; set; }

        //public ICollection<Intervention> Interventions { get; set; }
    }
}
