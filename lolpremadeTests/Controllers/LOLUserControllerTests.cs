using lolpremade.API;
using lolpremade.Data;
using lolpremade.Models;
using lolpremade.Utils.Services;
using lolpremade.ViewModels;
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
    public class LOLUserControllerTests
    {
        LolpremadeContext DbTestContext;
        RiotAPIService apiService;

        [TestInitialize]
        public void InitializeTestContext()
        {
            DbContextOptionsBuilder<LolpremadeContext> options = new DbContextOptionsBuilder<LolpremadeContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lolpremadeDBTest;Trusted_Connection=True;MultipleActiveResultSets=true");
            apiService = new RiotAPIService();

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
        public void RetrieveInformationFromUser()
        {
            var controller = new LOLUserAPIController(DbTestContext, apiService);
            LOLUserSyncRequest syncRequest = new LOLUserSyncRequest();
            syncRequest.lolusername = "aixaladacésar";
            syncRequest.username = "paco";
            var result = controller.Post(syncRequest);

            Assert.IsTrue(typeof(OkObjectResult) == result.GetType());
        }
    }
}
