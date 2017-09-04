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
    [Route("api/useropinions")]
    public class UserOpinionAPIController : Controller
    {
        UnitOfWork unitOfWork;

        public UserOpinionAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<UserOpinion> Get()
        {
            return unitOfWork.UsersOpinionsRepository.Get();
        }

        [HttpGet("{id}")]
        public IEnumerable<UserOpinion> Get(int id)
        {
            return unitOfWork.UsersOpinionsRepository.Get((o => o.ReceivingUserId == id));
        }

        // POST api/values
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]UserOpinion opinion)
        {
            if(ModelState.IsValid && opinion != null)
            {
                opinion.opinionDate = DateTime.Now;
                unitOfWork.UsersOpinionsRepository.Insert(opinion);
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
