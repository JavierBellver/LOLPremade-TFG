using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.Models
{
    public class UserPositionInTeam
    {
        [Key]
        public int ID { get; set; }
        public int TeamID { get; set; }
        public string Role { get; set; }
        public bool isSubstitute { get; set; }
    }
}
