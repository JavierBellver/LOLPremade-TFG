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
    [Route("api/messages")]
    public class MessagesAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public MessagesAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("{id}")]
        public IEnumerable<Message> Get(int id)
        {
            return unitOfWork.MessagesRepository.Get((m => m.ReceivingUserId == id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            if(ModelState.IsValid && message != null)
            {
                message.MessageDate = DateTime.Now;
                unitOfWork.MessagesRepository.Insert(message);
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
