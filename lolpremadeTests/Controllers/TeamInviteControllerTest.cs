using lolpremade.API;
using lolpremade.Data;
using lolpremade.Models;
using lolpremade.Utils;
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
    public class TeamInviteControllerTest
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
        public void SendTeamInvite()
        {
            TeamInvitation testInvite = new TeamInvitation
            {
                SenderTeamId = 1,
                ReceiverUserId = 1,
                IssuedDate = new DateTime(),
                Accepted = false
            };
            var controller = new TeamInvitesAPIController(DbTestContext);
            var result = controller.Post(testInvite);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void AcceptTeamInvite()
        {
            var controller = new TeamInvitesAPIController(DbTestContext);
            TeamInvitation invite = controller.Get().First();
            invite.Accepted = true;
            var result = controller.Put(invite.ID,invite);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }
    }
}
