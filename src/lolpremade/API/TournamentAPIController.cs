using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.Models;
using lolpremade.DAL;
using lolpremade.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/tournaments")]
    public class TournamentAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public TournamentAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            return unitOfWork.TournamentRepository.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Tournament Get(string id)
        {
            Tournament searchedTournament = new Tournament();
            int idnumber;
            if(int.TryParse(id,out idnumber))
            {
                searchedTournament = unitOfWork.TournamentRepository.GetById(idnumber);
            }
            else
            {
                IEnumerable<Tournament> candidates = unitOfWork.TournamentRepository.Get((t => t.Name == id));
                if(candidates.Any())
                {
                    searchedTournament = candidates.First();
                }
            }
            return searchedTournament;
        }
    }
}
