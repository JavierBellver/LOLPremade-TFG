using lolpremade.API;
using lolpremade.Data;
using lolpremade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class TeamControllerTests
    {

        LolpremadeContext DbTestContext;

        [TestInitialize]
        public void InitializeTestContext()
        {
            DbContextOptionsBuilder<LolpremadeContext> options = new DbContextOptionsBuilder<LolpremadeContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lolpremadeDBTest;Trusted_Connection=True;MultipleActiveResultSets=true");

            DbTestContext = new LolpremadeContext(options.Options);
            DbTestContext.Database.EnsureCreated();
            DbInitializer.TestInitiaize(DbTestContext);
        }

        [TestCleanup]
        public void TearDownTestContext()
        {
            string path = Directory.GetCurrentDirectory() + "\\sqlfixtures\\dropdb.sql";
            DbTestContext.Database.ExecuteSqlCommand(File.ReadAllText(path));
        }

        [TestMethod]
        public void GetAllTeamsTest()
        {
            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Get();

            Assert.IsTrue(((ICollection<Team>)result).Any());
        }

        [TestMethod]
        public void GetTeamByIdTest()
        {
            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Get("1");

            Assert.IsTrue(((Team)result).ID == 1);
        }

        [TestMethod]
        public void GetTeamByName()
        {
            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Get("PepeTeam");

            Assert.IsTrue(((Team)result).Name == "PepeTeam");
        }

        [TestMethod]
        public void CreateTeamTest()
        {
            Team teamToCreate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such good boys",
                teamAdmin = 3
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Post(teamToCreate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void CreateTeamWithTeammates()
        {
            Team teamToCreate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such good boys",
                teamAdmin = 3
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Post(teamToCreate, "inviteman1,inviteman2");

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ModifyTeamDescriptionTest()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boyz",
                teamAdmin = 0
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ModifyTeamsRank()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boys",
                TeamRank = "Platinum V"
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ModifyTeamsCountry()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boys",
                TeamRank = Rank.BronzeII.Value
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ModifyTeamsLanguage()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boys",
                Language = Language.Portuguese.Value
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ModifyTeamsRegion()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boys",
                PlayRegion = PlayRegion.EUWEST.Value
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ChangeTeamsAdmin()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such bad boys",
                teamAdmin = 1
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void DeletePlayerFromTeamFailsPlayerNotOnTeam()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such good boys",
                teamAdmin = 1
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate, 3);

            Assert.IsTrue(typeof(BadRequestObjectResult) == result.GetType());
        }

        [TestMethod]
        public void DeletePlayerFromTeam()
        {
            Team teamToUpdate = new Team
            {
                Name = "GoodBoys",
                Description = "We're such good boys",
                teamAdmin = 1
            };

            var controller = new TeamsAPIController(DbTestContext);
            var result = controller.Put(1, teamToUpdate, 2);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

    }
}
