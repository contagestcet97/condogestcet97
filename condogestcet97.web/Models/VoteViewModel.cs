using condogestcet97.web.Data.Entities.Condominium;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace condogestcet97.web.Models
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int MeetingId { get; set; }

        public bool IsApproved { get; set; }

        public int TotalVotes => VotesInFavour + VotesAgainst + VotesAbstained;

        public int VotesInFavour { get; set; }

        public int VotesAgainst { get; set; }

        public int VotesAbstained { get; set; }
    }
}
