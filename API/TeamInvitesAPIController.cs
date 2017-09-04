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
    [Route("api/teaminvites")]
    public class TeamInvitesAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public TeamInvitesAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public IEnumerable<TeamInvitation> Get()
        {
            return unitOfWork.TeamInvitationsRepository.Get();
        }

        [HttpGet("{id}")]
        public IEnumerable<TeamInvitation> Get(string id)
        {
            IEnumerable<TeamInvitation> invites = null;
            int idnumber;
            if (int.TryParse(id, out idnumber))
            {
                invites = unitOfWork.TeamInvitationsRepository.Get((i => i.ID == idnumber));
            }
            else
            {
                int receivingUserId = unitOfWork.UserRepository.Get((u => u.Username == id)).First().ID;
                invites = unitOfWork.TeamInvitationsRepository.Get((i => i.ReceiverUserId == receivingUserId));
            }
            return invites;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]TeamInvitation invite)
        {
            if(ModelState.IsValid)
            {
                invite.IssuedDate = DateTime.Now;
                invite.Accepted = false;
                unitOfWork.TeamInvitationsRepository.Insert(invite);
                unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TeamInvitation invite)
        {
            if(ModelState.IsValid)
            {
                invite.ID = id;
                unitOfWork.TeamInvitationsRepository.Update(invite);
                if(invite.Accepted)
                {
                    User receiverUser = unitOfWork.UserRepository.GetById(invite.ReceiverUserId);
                    Team senderTeam = unitOfWork.TeamRepository.GetById(invite.SenderTeamId);
                    receiverUser.PertainingTeam = invite.SenderTeamId;
                    unitOfWork.TeamRepository.Update(senderTeam);
                }
                unitOfWork.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                unitOfWork.TeamInvitationsRepository.Delete(id);
                unitOfWork.Save();
                return Ok("Succesfully deleted invitation");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
