using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.ViewModels
{
    public class LeaveTeamRequest
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
