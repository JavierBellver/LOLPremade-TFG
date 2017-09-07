using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.ViewModels
{
    public class UserAvatarUpload
    {
        public string UserName { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
