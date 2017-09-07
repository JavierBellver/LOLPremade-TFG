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
    public class MessagesControllerTests
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
        public void SendMessage()
        {
            var controller = new MessagesAPIController(DbTestContext);
            Message newMessage = new Message
            {
                SenderUserId = 1,
                ReceivingUserId = 2,
                Subject = "This is the message",
                MessageText = "This is the main text of the message",
                MessageDate = DateTime.Now
            };
            var result = controller.Post(newMessage);
            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void ReadMessages()
        {
            var controller = new MessagesAPIController(DbTestContext);
            var messages = controller.Get(2);
            Assert.IsTrue(messages.Any());
        }
    }
}
