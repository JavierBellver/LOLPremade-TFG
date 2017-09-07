using lolpremade.Data;
using lolpremade.Models;
using lolpremade.Utils.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremadeTests
{
    [TestClass]
    public class RiotAPIServiceTests
    {
        RiotAPIService riotAPI;

        [TestInitialize]
        public void InitializeTestContext()
        {
            riotAPI = new RiotAPIService();
        }

        [TestMethod]
        public void GetLOLUserBasicDataUsernameWithSpaces()
        {
            string summonerName = "El Tito Shen";
            var userInfo = riotAPI.GetBasicUserInfo("euw1", summonerName);

            Assert.IsTrue(userInfo.Name == summonerName+" ");
        }

        [TestMethod]
        public void GetLOLUserBasicDataUsernameWithStrangeCharacters()
        {
            string summonerName = "aixaladacésar";
            var userInfo = riotAPI.GetBasicUserInfo("euw1", summonerName);

            Assert.IsTrue(userInfo.Name == summonerName);
        }

        [TestMethod]
        public void GetLOLUserTierAndRank()
        {
            string summonerId = "97358047";
            string[] tierAndRank = new string[2];

            tierAndRank = riotAPI.GetTierAndRank("euw1", summonerId);
            Assert.IsTrue(tierAndRank[0] == "BRONZE" && tierAndRank[1] == "V");
        }

        [TestMethod]
        public void GetLOLCompleteUserInfo()
        {
            string summonerName = "aixaladacésar";
            var userInfo = riotAPI.GetCompleteUserBySummonerName(summonerName, "Europe West");

            Assert.IsTrue(userInfo.Name == summonerName && userInfo.Tier == "BRONZE");
        }

        [TestMethod]
        public void AuthorizeUsersRunePage()
        {
            string summonerName = "aixaladacésar", region = "Europe West", masteryPage = "Ashe";
            Assert.IsTrue(riotAPI.AuthorizeLOLAccount(summonerName, region, masteryPage));
        }
    }
}
