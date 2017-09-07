using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.Models;
using Microsoft.AspNetCore.Authorization;
using lolpremade.Data;
using lolpremade.DAL;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/tournamentparticipants")]
    public class TournamentParticipantsAPIController : Controller
    {
        UnitOfWork unitOfWork;

        public TournamentParticipantsAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<TournamentParticipant> Get(int id)
        {
            return unitOfWork.TournamentParticipantRepository.Get((p => p.TournamentId == id));
        }

        // POST api/values
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]TournamentParticipant participant)
        {
            if(ModelState.IsValid && participant != null)
            {
                Tournament tournament = unitOfWork.TournamentRepository.GetById(participant.TournamentId);
                if(tournament.NumberOfCurrentParticipants < tournament.NumberOfParticipantTeams)
                {
                    tournament.NumberOfCurrentParticipants = tournament.NumberOfCurrentParticipants + 1;
                    unitOfWork.TournamentRepository.Update(tournament);
                    unitOfWork.TournamentParticipantRepository.Insert(participant);
                    unitOfWork.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
