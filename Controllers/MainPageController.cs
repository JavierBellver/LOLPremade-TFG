using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using lolpremade.ViewModels;
using lolpremade.Data;
using lolpremade.DAL;
using lolpremade.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.Controllers
{
    public class MainPageController : Controller
    {
        private IHostingEnvironment environment;
        private UnitOfWork unitOfWork;

        public MainPageController(IHostingEnvironment _environment,LolpremadeContext context)
        {
            environment = _environment;
            unitOfWork = new UnitOfWork(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadTeamAvatar")]
        [Authorize]
        public IActionResult uploadTeamAvatar(TeamAvatarUpload AvatarUpload)
        {
            if (AvatarUpload.Avatar == null) return BadRequest(new { response = "Error, file is empty" });
            if (AvatarUpload.Avatar.Length == 0) return BadRequest(new { response = "Error, file is empty" });

            string extension = AvatarUpload.Avatar.ContentType.Split('/')[1];
            string filepath = Path.Combine(environment.WebRootPath, "images", "teams");
            string filename = AvatarUpload.TeamName + "avatar." + extension;

            using (var stream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
            {
                AvatarUpload.Avatar.CopyTo(stream);
                Team teamToUploadAvatar = unitOfWork.TeamRepository.Get((t => t.Name == AvatarUpload.TeamName)).First();
                teamToUploadAvatar.TeamAvatar = stream.ToString();
                unitOfWork.TeamRepository.Update(teamToUploadAvatar);
                unitOfWork.Save();
            }
            return Ok(new { response = "Success uploading team avatar" });
        }

        [HttpPost("UploadUserAvatar")]
        [Authorize]
        public IActionResult uploadUserAvatar(UserAvatarUpload AvatarUpload)
        {
            if (AvatarUpload.Avatar == null) return BadRequest(new { response = "Error, file is empty" });
            if (AvatarUpload.Avatar.Length == 0) return BadRequest(new { response = "Error, file is empty" });

            string extension = AvatarUpload.Avatar.ContentType.Split('/')[1];
            string filepath = Path.Combine(environment.WebRootPath, "images", "users");
            string filename = AvatarUpload.UserName + "avatar."+extension;

            using (var stream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
            {
                AvatarUpload.Avatar.CopyTo(stream);
                User userToUploadAvatar = unitOfWork.UserRepository.Get((u => u.Username == AvatarUpload.UserName)).First();
                userToUploadAvatar.UserAvatar = stream.ToString();
                unitOfWork.UserRepository.Update(userToUploadAvatar);
                unitOfWork.Save();
            }
            return Ok(new { response = "Success uploading user avatar" });
        }
    }
}
