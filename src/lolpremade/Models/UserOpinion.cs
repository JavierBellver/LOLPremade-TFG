using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class UserOpinion
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("OpinionUserRefId")]
        public int SenderUserId { get; set; }
        public User UserThatOpinions { get; set; }
        [Required]
        [ForeignKey("ReceivingUserOpinionUserRefId")]
        public int ReceivingUserId { get; set; }
        public User ReceivingUserOpinion { get; set; }
        public double punctuation { get; set; }
        public string shortText { get; set; }
        public string commentText { get; set; }
        public DateTime opinionDate { get; set; }
    }
}
