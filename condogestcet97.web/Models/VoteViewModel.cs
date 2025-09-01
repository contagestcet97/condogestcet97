using condogestcet97.web.Data.Entities.Condominium;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public IEnumerable<SelectListItem>? Meetings { get; set; }
    }
}
