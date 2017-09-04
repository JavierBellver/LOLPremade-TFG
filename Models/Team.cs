using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class Team
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("UserAdminRefId")]
        public int teamAdmin { get; set; }
        public User Admin { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string TeamRank { get; set; }
        public string PlayRegion { get; set; }
        string teamAvatar = "images/add_friend_article_banner.jpg";
        public string TeamAvatar { get { return this.teamAvatar; } set { this.teamAvatar = value; } }
    }
}
