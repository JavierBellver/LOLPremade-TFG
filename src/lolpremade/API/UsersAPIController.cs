using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.Models;
using lolpremade.DAL;
using lolpremade.Data;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using lolpremade.Utils;
using lolpremade.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/users")]
    public class UsersAPIController : Controller
    {
        private UnitOfWork unitOfWork;

        public UsersAPIController(LolpremadeContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: api/users
        [HttpGet]
        public IEnumerable<UserPublicInformation> Get([FromQuery]int pagenumber = 0, [FromQuery]int pagesize = 9, [FromQuery]int searchByTeamId = 0)
        {
            var allUsers = unitOfWork.UserRepository.Get();
            if (searchByTeamId != 0)
            {
                allUsers = allUsers.Where(u => u.PertainingTeam == searchByTeamId);
            }
            if (pagenumber == 0)
            {
                List<UserPublicInformation> allUsersInfo = new List<UserPublicInformation>();
                for(int i = 0;i < allUsers.Count(); i++)
                {
                    allUsersInfo.Add(new UserPublicInformation(allUsers.ElementAt(i)));
                }
                return allUsersInfo;
            }
            else
            {
                List<User> usersToReturn = new List<User>();
                int startingUser = (pagenumber - 1) * pagesize;
                int finnishingUser = startingUser + pagesize;
                if (startingUser < (allUsers.Count() - 1) && finnishingUser < (allUsers.Count() - 1))
                {
                    for (int i = startingUser; i < finnishingUser; i++)
                    {
                        var user = allUsers.ElementAt(i);
                        usersToReturn.Add(user);
                    }
                }
                else if (startingUser == 0 && finnishingUser > (allUsers.Count() - 1))
                {
                    foreach (User u in allUsers)
                    {
                        usersToReturn.Add(u);
                    }
                }
                List<UserPublicInformation> allUsersInfo = new List<UserPublicInformation>();
                for (int i = 0; i < usersToReturn.Count(); i++)
                {
                    allUsersInfo.Add(new UserPublicInformation(usersToReturn.ElementAt(i)));
                }
                return allUsersInfo;
            }
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public UserPublicInformation Get(string id)
        {
            User searchedUser = new User();
            int idnumber;
            if (int.TryParse(id, out idnumber))
            {
                searchedUser = unitOfWork.UserRepository.Get((u => u.ID == idnumber)).First();
            }
            else
            {
                IEnumerable<User> candidates = unitOfWork.UserRepository.Get((u => u.Email == id));
                if (candidates.Any())
                {
                    searchedUser = candidates.First();
                }
                else
                {
                    candidates = unitOfWork.UserRepository.Get((u => u.Username == id));
                    if (candidates.Any())
                    {
                        searchedUser = candidates.First();
                    }
                }
            }
            return new UserPublicInformation(searchedUser);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody]User newUser)
        {
            if (ModelState.IsValid)
            {
                if (newUser != null)
                {
                    try
                    {
                        newUser.Salt = CryptoUtils.GetSalt();
                        newUser.Password = CryptoUtils.HashWithSHA256(newUser.Password, newUser.Salt);
                        newUser.PlayRegion = "";
                        newUser.Country = "";
                        newUser.Language = "";
                        newUser.Rank = "";
                        newUser.Role = "";
                        newUser.Level = "";
                        newUser.DateOfBirth = new DateTime();
                        unitOfWork.UserRepository.Insert(newUser);
                        unitOfWork.Save();
                        return Ok();
                    } catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        return BadRequest(new { message = "Error, email or username already exists" });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Error, empty fields sent" });
                }
            }
            else
            {
                return BadRequest(new { message = "Error, incorrect fields" });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody]User modifiedUser)
        {
            if (ModelState.IsValid)
            {
                User userToModify = unitOfWork.UserRepository.Get((u => u.ID == id)).First();
                userToModify.DateOfBirth = modifiedUser.DateOfBirth;
                userToModify.Country = modifiedUser.Country;
                userToModify.Language = modifiedUser.Language;
                userToModify.PlayRegion = modifiedUser.PlayRegion;
                userToModify.Role = modifiedUser.Role;
                unitOfWork.UserRepository.Update(userToModify);
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
