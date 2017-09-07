using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class TeamInvitation
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("OfferingTeamRefId")]
        public int SenderTeamId { get; set; }
        public Team TeamToInvite { get; set; }
        [Required]
        [ForeignKey("ReceivingUserRefId")]
        public int ReceiverUserId { get; set; }
        public User ReceivingUser { get; set; }
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public bool Accepted { get; set; }
    }
}
