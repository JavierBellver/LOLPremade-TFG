using lolpremade.API;
using lolpremade.Data;
using lolpremade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremadeTests.Controllers
{
    [TestClass]
    public class TournamentParticipantsControllerTests
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
        public void SignToTournament()
        {
            var controller = new TournamentParticipantsAPIController(DbTestContext);
            TournamentParticipant newParticipant = new TournamentParticipant();
            newParticipant.TeamId = 1;
            newParticipant.TournamentId = 1;
            var result = controller.Post(newParticipant);
            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void GetTournamentParticipantsByTournamentId()
        {
            var controller = new TournamentParticipantsAPIController(DbTestContext);
            var result = controller.Get(1);
            Assert.IsTrue(result.Any());
        }
    }
}
