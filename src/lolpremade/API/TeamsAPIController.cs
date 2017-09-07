using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.DAL;
using lolpremade.Data;
using Microsoft.AspNetCore.Authorization;
using lolpremade.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/teams")]
    public class TeamsAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public TeamsAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: api/teams
        [HttpGet]
        public IEnumerable<Team> Get([FromQuery]int pagenumber=0,[FromQuery]int pagesize=9)
        {
            if(pagenumber == 0)
            {
                return unitOfWork.TeamRepository.Get();
            }
            var allTeams = unitOfWork.TeamRepository.Get();
            List<Team> teamsToReturn = new List<Team>();
            int startingTeam = (pagenumber - 1) * pagesize;
            int finnishingTeam = startingTeam + pagesize;
            if (startingTeam < (allTeams.Count() - 1) && finnishingTeam < (allTeams.Count() - 1))
            {
                for (int i = startingTeam; i < finnishingTeam; i++)
                {
                    var team = allTeams.ElementAt(i);
                    teamsToReturn.Add(team);
                }
            }
            return teamsToReturn;
        }

        // GET api/teams/{id}
        [HttpGet("{id}")]
        public Team Get(string id)
        {
            Team toReturn = new Team();
            int idnumber;
            if(int.TryParse(id,out idnumber))
            {
                toReturn = unitOfWork.TeamRepository.Get((t => t.ID == idnumber)).First();
            }
            else
            {
                IEnumerable<Team> candidates = unitOfWork.TeamRepository.Get((t => t.Name == id));
                if (candidates.Any())
                {
                    toReturn = candidates.First();
                }
            }
            return toReturn;
        }

        // POST api/teams
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody]Team newTeam,[FromQuery]string usersToInvite = "")
        {
            if (ModelState.IsValid)
            {
                if (newTeam != null)
                {
                    newTeam.Description = "";
                    newTeam.Country = "";
                    newTeam.Language = "";
                    newTeam.PlayRegion = "";
                    newTeam.TeamRank = "";
                    unitOfWork.TeamRepository.Insert(newTeam);
                    unitOfWork.Save();
                    User teamAdmin = unitOfWork.UserRepository.GetById(newTeam.teamAdmin);
                    teamAdmin.PertainingTeam = unitOfWork.TeamRepository.Get((t => t.Name == newTeam.Name)).First().ID;
                    unitOfWork.UserRepository.Update(teamAdmin);
                    unitOfWork.Save();
                    if (usersToInvite != "")
                    {
                        Team teamToInvite = unitOfWork.TeamRepository.Get((t => t.Name == newTeam.Name)).First();
                        string[] usernames = usersToInvite.Split(',');
                        foreach(string username in usernames)
                        {
                            if(unitOfWork.UserRepository.Get((u => u.Username == username)).Any())
                            {
                                User userToInvite = unitOfWork.UserRepository.Get((u => u.Username == username)).First();
                                TeamInvitation newInvite = new TeamInvitation
                                {
                                    SenderTeamId = teamToInvite.ID,
                                    ReceiverUserId = userToInvite.ID,
                                    Accepted = false,
                                    IssuedDate = DateTime.Now
                                };
                                unitOfWork.TeamInvitationsRepository.Insert(newInvite);
                            }
                        }
                        unitOfWork.Save();
                    }
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

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody]Team newTeamData,[FromQuery]int userToDelete = 0)
        {
            if(ModelState.IsValid)
            {
                Team modifiedTeam = unitOfWork.TeamRepository.GetById(id);
                if (userToDelete != 0)
                {
                    User toDelete = unitOfWork.UserRepository.Get((u => u.ID == userToDelete)).First();
                    if (toDelete.PertainingTeam == modifiedTeam.ID)
                    {
                        toDelete.PertainingTeam = 0;
                        unitOfWork.UserRepository.Update(toDelete);
                    }
                    else
                    {
                        return BadRequest("Error, the user to delete doesn't pertain to the team");
                    }
                }
                modifiedTeam.teamAdmin = newTeamData.teamAdmin;
                modifiedTeam.Description = newTeamData.Description;
                modifiedTeam.Language = newTeamData.Language;
                modifiedTeam.Country = newTeamData.Country;
                modifiedTeam.PlayRegion = newTeamData.PlayRegion;
                modifiedTeam.TeamRank = newTeamData.TeamRank;
                unitOfWork.TeamRepository.Update(modifiedTeam);
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
