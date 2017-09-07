using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class Tournament
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Rank { get; set; }
        public string OrganizerName { get; set; }
        public int NumberOfParticipantTeams { get; set; }
        public int NumberOfCurrentParticipants { get; set; }
        string tournamentAvatar = "images/add_friend_article_banner.jpg";
        public string TournamentAvatar { get { return this.tournamentAvatar; } set { this.tournamentAvatar = value; } }
    }
}
