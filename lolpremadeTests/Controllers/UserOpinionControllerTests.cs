using lolpremade.API;
using lolpremade.Data;
using lolpremade.Models;
using lolpremade.Utils.Services;
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
    public class UserOpinionControllerTests
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
        public void WriteNewComment()
        {
            var controller = new UserOpinionAPIController(DbTestContext);
            UserOpinion newOpinion = new UserOpinion();
            newOpinion.SenderUserId = 1;
            newOpinion.ReceivingUserId = 2;
            newOpinion.opinionDate = DateTime.Now;
            newOpinion.punctuation = 3.5;
            newOpinion.shortText = "Not bad at all";
            newOpinion.commentText = "Lorem Ipsum Ipsum ipsum Lorem Ipsum is just a text used for Lorem Ipsum Lorem Ipsum";
            var result = controller.Post(newOpinion);
            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ReadComments()
        {
            var controller = new UserOpinionAPIController(DbTestContext);
            IEnumerable<UserOpinion> userOpinions = controller.Get();
            Assert.IsTrue(userOpinions.Any());
        }
    }
}
