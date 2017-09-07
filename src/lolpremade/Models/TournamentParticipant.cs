using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class TournamentParticipant
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("ParticipantTeamRefId")]
        public int TeamId { get; set; }
        public Team ParticipantTeam { get; set; }
        [Required]
        [ForeignKey("TournamentRefId")]
        public int TournamentId { get; set; }
        public Tournament TournamentToParticipate { get; set; }
    }
}
