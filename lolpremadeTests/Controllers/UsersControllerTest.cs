using lolpremade.API;
using lolpremade.DAL;
using lolpremade.Data;
using lolpremade.Models;
using lolpremade.Utils;
using lolpremade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremadeTests
{
    [TestClass]
    public class UsersControllerTest
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
        public void GetAUserByNameTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            var result = controller.Get("paco");

            Assert.IsTrue(((UserPublicInformation)result).Username == "paco");
        }

        [TestMethod]
        public void GetAUserByEmailTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            var result = controller.Get("pedro@gmail.com");

            Assert.IsTrue(((UserPublicInformation)result).Email == "pedro@gmail.com");
        }

        [TestMethod]
        public void GetAUserByIdTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            var result = controller.Get("1");

            Assert.IsTrue(((UserPublicInformation)result).ID == 1);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            var result = controller.Get();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateNewUserTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            User userTest = new User();
            userTest.Email = "pedro@hashtag.com";
            userTest.Username = "PeterMagnificus";
            userTest.Password = "MAGNIFICUSDICKUS";
            
            var result = controller.Post(userTest);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var controller = new UsersAPIController(DbTestContext);
            User modifiedUser = new User();
            modifiedUser.Country = "Spain";
            modifiedUser.Language = "Spanish";
            modifiedUser.PlayRegion = "Europe West";
            modifiedUser.DateOfBirth = DateTime.Now;

            var result = controller.Put(1, modifiedUser);

            Assert.IsTrue(typeof(OkResult) == result.GetType());
        }
    }
}
