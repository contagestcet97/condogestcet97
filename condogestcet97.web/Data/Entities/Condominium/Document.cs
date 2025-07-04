using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public enum DocumentType
    {
        Meeting,
        Intervention
    }

    public abstract class Document : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(25)")]
        [MaxLength(25)]
        public string Subject { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime EmissionDate { get; set; }

        public abstract DocumentType Type { get; set; }

    }
}
