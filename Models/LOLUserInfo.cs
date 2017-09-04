using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class LOLUserInfo
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User"), Required]
        public int UserId { get; set; }
        public User PertainingUser { get; set; }
        public string Name { get; set; }
        public string Summonerlvl { get; set; }
        public string AccountId { get; set; }
        public string SummonerId { get; set; }
        public string Tier { get; set; }
        public string Rank { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Authorized { get; set; }
        public string HashToAuthorize { get; set; }
    }
}
