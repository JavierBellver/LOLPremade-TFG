using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.Models;
using lolpremade.DAL;
using lolpremade.Data;
using Microsoft.AspNetCore.Authorization;

namespace lolpremade.API
{
    [Route("api/userposition")]
    public class UserPositionInTeamAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public UserPositionInTeamAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        [HttpGet("{username}")]
        public UserPositionInTeam Get(string username)
        {
            User userToRetrieve = unitOfWork.UserRepository.Get((u => u.Username == username),null, "PositionOnTeam").First();
            return userToRetrieve.PositionOnTeam;
        }

        [HttpPut("{username}")]
        [Authorize]
        public IActionResult Put(string username, [FromBody]UserPositionInTeam newPosition)
        {
            if(ModelState.IsValid)
            {
                User userToRetrieve = unitOfWork.UserRepository.Get((u => u.Username == username),null,"PositionOnTeam").First();
                if(userToRetrieve.PositionOnTeam == null)
                {
                    userToRetrieve.PositionOnTeam = new UserPositionInTeam()
                    {
                        TeamID = userToRetrieve.PertainingTeam,
                        Role = "ADC",
                        isSubstitute = false
                    };
                    unitOfWork.UserPositionInTeamRepository.Insert(userToRetrieve.PositionOnTeam);
                    unitOfWork.UserRepository.Update(userToRetrieve);
                    unitOfWork.Save();
                }
                int posId = userToRetrieve.PositionOnTeam.ID;
                UserPositionInTeam userPositionToModify = unitOfWork.UserPositionInTeamRepository.Get((p => p.ID == posId)).First();
                userPositionToModify.Role = newPosition.Role;
                userPositionToModify.isSubstitute = newPosition.isSubstitute;
                unitOfWork.UserPositionInTeamRepository.Update(userPositionToModify);
                unitOfWork.UserRepository.Update(userToRetrieve);
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
