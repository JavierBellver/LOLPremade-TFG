using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.ViewModels
{
    public class TeamAvatarUpload
    {
        public string TeamName { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
