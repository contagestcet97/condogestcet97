using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Data.Entities.Condominium
{
    public class Vote : IEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(250)")]
        [MaxLength(250)]
        public string Description { get; set; }

        public Meeting Meeting { get; set; }

        public bool IsApproved { get; set; }

        public int TotalVotes => VotesInFavour + VotesAgainst + VotesAbstained;

        public int VotesInFavour {  get; set; }

        public int VotesAgainst {  get; set; }

        public int VotesAbstained { get; set; }
    }
}
