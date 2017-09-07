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
    public class TournamentControllerTest
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
        public void GetTournamentById()
        {
            var controller = new TournamentAPIController(DbTestContext);
            var result = controller.Get("1");

            Assert.IsTrue(result.Name == "MegaTournament");
        }

        [TestMethod]
        public void GetTournamentByName()
        {
            var controller = new TournamentAPIController(DbTestContext);
            var result = controller.Get("MegaTournament");

            Assert.IsTrue(result.Name == "MegaTournament");
        }
    }
}
