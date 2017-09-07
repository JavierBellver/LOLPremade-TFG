using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lolpremade.DAL;
using lolpremade.Data;
using lolpremade.Utils.Services;
using Microsoft.AspNetCore.Authorization;
using lolpremade.Models;
using lolpremade.Utils;
using lolpremade.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lolpremade.API
{
    [Route("api/lolusersinfo")]
    public class LOLUserAPIController : Controller
    {
        private UnitOfWork unitOfWork;
        private IRiotAPIService riotAPIService;

        public LOLUserAPIController(LolpremadeContext context, IRiotAPIService _riotAPIService)
        {
            unitOfWork = new UnitOfWork(context);
            riotAPIService = _riotAPIService;
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]LOLUserSyncRequest syncRequest) //Hacer saltar un error en caso de clave invalida
        {
            try
            {
                User userToSync = unitOfWork.UserRepository.Get((u => u.Username == syncRequest.username)).First();
                if (userToSync.PlayRegion == "")
                {
                    return BadRequest(new { response = "Error, user doesn't have an assigned region" });
                }
                IEnumerable<LOLUserInfo> previousInfo = unitOfWork.LOLUsersInfoRepository.Get((i => i.Name == syncRequest.lolusername));
                if (previousInfo.Any())
                {
                    return Ok(new { response = "Success data sync request register", key = previousInfo.First().HashToAuthorize });
                }
                LOLUserInfo newInfo = riotAPIService.GetCompleteUserBySummonerName(syncRequest.lolusername, userToSync.PlayRegion);
                if(newInfo == new LOLUserInfo())
                {
                    return BadRequest(new { response = "Error, coudln't access League of Legends API" });
                }
                newInfo.UserId = userToSync.ID;
                newInfo.LastUpdated = DateTime.Now;
                newInfo.Authorized = false;
                newInfo.HashToAuthorize = CryptoUtils.GetPseudoRandomString(18);
                unitOfWork.LOLUsersInfoRepository.Insert(newInfo);
                unitOfWork.Save();
                return Ok(new { response = "Success data sync request register", key=newInfo.HashToAuthorize });
            }
            catch(Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
            {
                return BadRequest(new { response = "Error, couldn't find user" });
            }
        }

        // PUT api/values/5
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]LOLUserSyncRequest syncRequest, [FromQuery]bool authorize = false)
        {
            try
            {
                User userToSync = unitOfWork.UserRepository.Get((u => u.Username == syncRequest.username)).First();
                if(userToSync.PlayRegion == "")
                {
                    return BadRequest(new { response = "Error, user doesn't have an assigned region" });
                }
                LOLUserInfo newInfo = unitOfWork.LOLUsersInfoRepository.Get((u => u.UserId == userToSync.ID)).First();
                if (authorize)
                {
                    if (riotAPIService.AuthorizeLOLAccount(syncRequest.lolusername, userToSync.PlayRegion, newInfo.HashToAuthorize))
                    {
                        newInfo.LastUpdated = DateTime.Now;
                        newInfo.Authorized = true;
                        if(newInfo.Tier == "")
                        {
                            userToSync.Rank = "UNRANKED";
                        }
                        else
                        {
                            userToSync.Rank = newInfo.Tier + " " + newInfo.Rank;
                        }
                        userToSync.Level = newInfo.Summonerlvl;
                        unitOfWork.UserRepository.Update(userToSync);
                        unitOfWork.LOLUsersInfoRepository.Update(newInfo);
                        unitOfWork.Save();
                        return Ok(new { response = "Succesful authentication" });
                    }
                    else
                    {
                        return Ok(new { response = "Error on authentication" });
                    }
                }
                else
                {
                    newInfo.LastUpdated = DateTime.Now;
                    newInfo.Authorized = false;
                    newInfo.HashToAuthorize = CryptoUtils.GetPseudoRandomString(18);
                    return Ok(new { response = "Success data sync request registered", key = newInfo.HashToAuthorize });
                }
            }
            catch(Exception e) when (e is ArgumentNullException || e is InvalidOperationException)
            {
                return BadRequest(new { response = "Error, couldn't find user" });
            }
        }
    }
}
