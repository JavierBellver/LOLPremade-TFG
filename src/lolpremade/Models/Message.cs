using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("SenderUserRefId")]
        public int? SenderUserId { get; set; }
        public User SenderUser { get; set; }
        [ForeignKey("ReceivingUserRefId")]
        public int? ReceivingUserId { get; set; }
        public User ReceivingUser { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; } 
        public DateTime MessageDate { get; set; }
    }
}
