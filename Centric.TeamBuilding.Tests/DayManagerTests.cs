using System;
using System.Linq;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using Centric.TeamBuilding.Tests.Repository;
using Centric.TeamBuilding.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Centric.TeamBuilding.Tests
{
    [TestClass]
    public class DayManagerTests
    {
        private const string TestDescription = "inserted from test";
        [TestMethod]
        public void ShouldCreateDay()
        {
            Cleanup();
            var dayManager = new DayManager(new DayRepository(), new UserRepository());
            var testRepository = new TestRepository();
            TestUtils.InsertTestStaffUser();
            var numberOfDaysBeforeCreate = testRepository.GetAllDays().Count();
            var d = new Day
            {
                Date = new DateTime(2017, 12, 31),
                Description = TestDescription
            };
            dayManager.Create(d,TestUtils.TestStaffUserId);

            var numberOfDaysAfterCreate = testRepository.GetAllDays().Count();

            Assert.AreEqual(numberOfDaysBeforeCreate + 1, numberOfDaysAfterCreate);
        }

        [TestMethod]
        public void ShouldGetDays()
        {
            var dayManager = new DayManager(new DayRepository(), new UserRepository());
            TestUtils.InsertTestDay();
            var days = dayManager.GetDays().ToList();

            Assert.IsTrue(days.Any());
            Assert.AreEqual(TestUtils.TestDescription, days.First().Description);
            Assert.AreEqual(TestUtils.TestDayId, days.First().Id);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var repo = new TestRepository();
            repo.Cleanup();
        }
    }
}
