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
using System.Reflection;
using System.Threading.Tasks;

namespace lolpremadeTests.Controllers
{
    [TestClass]
    public class UserPositionInTeamControllerTests
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
            string path = Directory.GetCurrentDirectory()+"\\sqlfixtures\\dropdb.sql";
            DbTestContext.Database.ExecuteSqlCommand(File.ReadAllText(path));
        }

        [TestMethod]
        public void GetUserRoleInTeam()
        {
            string userName = "paco";
            var controller = new UserPositionInTeamAPIController(DbTestContext);
            var result = controller.Get(userName);
            Assert.IsTrue(result.Role == "ADC");
        }

        [TestMethod]
        public void ModifyUserRoleInTeam()
        {
            string userName = "paco";
            UserPositionInTeam toModify = new UserPositionInTeam();
            toModify.TeamID = 1;
            toModify.Role = "Support";
            toModify.isSubstitute = false;
            var controller = new UserPositionInTeamAPIController(DbTestContext);
            var result = controller.Put(userName, toModify);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }
    }
}
