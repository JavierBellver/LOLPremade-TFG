using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.Models;
using lolpremade.Data;
using lolpremade.DAL;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/teamjoinrequests")]
    public class TeamJoinRequestAPIController : Controller
    {
        UnitOfWork unitOfWork;

        public TeamJoinRequestAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Message requestJoinMessage)
        {
            if(ModelState.IsValid && requestJoinMessage != null)
            {
                requestJoinMessage.MessageDate = DateTime.Now;
                unitOfWork.MessagesRepository.Insert(requestJoinMessage);
                unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
