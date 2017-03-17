using System;
using System.Linq;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.Entities;
using Centric.TeamBuilding.Tests.Repository;
using Centric.TeamBuilding.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Centric.TeamBuilding.DataAccess.Repositories;

namespace Centric.TeamBuilding.Tests
{
    [TestClass]
    public class ActivityManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Cleanup();
            TestUtils.InsertTestDay();
            TestUtils.InsertTestStaffUser();
            TestUtils.InsertTestEmployeeUser();
        }

        [TestMethod]
        public void ShouldGetDayActivities()
        {
            
            TestUtils.InsertTestMainActivity();
            var activityManager = new ActivityManager(new ActivityRepository(), new UserRepository());

            var insertedActivity = activityManager.GetDayActivities(TestUtils.TestDayId).First();

            Assert.AreEqual(TestUtils.TestMainActivityId, insertedActivity.Id);
        }

        [TestMethod]
        public void ShouldCreateMainActivity()
        {
            var activityManager = new ActivityManager(new ActivityRepository(), new UserRepository());
            var testRepository = new TestRepository();
            var activity = new MainActivity
            {
                Title = "Test main activity title",
                StartTime = new DateTime(2016, 01, 01, 9, 30, 0),
                EndTime = new DateTime(2016, 01, 01, 20, 00, 0),
                Description = TestUtils.TestDescription,
                Location = "undeva",
                IsFreeActivity = false,
                CreatorId = TestUtils.TestStaffUserId,
                DayId = TestUtils.TestDayId
            };
            activityManager.CreateMainActivity(activity);

            var insertedActivity = testRepository.GetAllActivities().First();

            Assert.AreEqual(activity.CreatorId, insertedActivity.CreatorId);
            Assert.AreEqual(activity.EndTime, insertedActivity.EndTime);
        }

        [TestMethod]
        public void ShouldGetEmployeeActivities()
        {
            TestUtils.InsertTestMainActivity(true);
            var activityManager = new ActivityManager(new ActivityRepository(), new UserRepository());
            TestUtils.InsertTestEmployeeActivity();

            var insertedActivity = activityManager.GetEmployeeActivities(TestUtils.TestMainActivityId).First();

            Assert.AreEqual(TestUtils.TestEmployeeActivityId, insertedActivity.Id);
        }

        [TestMethod]
        public void ShouldCreateEmployeeActivity()
        {
            TestUtils.InsertTestMainActivity(true);
            var activityManager = new ActivityManager(new ActivityRepository(), new UserRepository());

            var newActivity = new EmployeeActivity()
            {
                Id = TestUtils.TestEmployeeActivityId,
                Description = TestUtils.TestDescription,
                CreatorId = TestUtils.TestEmployeeUserId,
                EndTime = DateTime.Now.AddMinutes(30),
                StartTime = DateTime.Now,
                Location = "u",
                DayId = TestUtils.TestDayId,
                Title = "Test activity",
                ParentId = TestUtils.TestMainActivityId
            };
            activityManager.CreateEmployeeActivity(newActivity);

            var allEmployeeActivities = activityManager.GetEmployeeActivities(TestUtils.TestMainActivityId);

            Assert.AreEqual(1, allEmployeeActivities.Count());
        }

        [TestMethod]
        public void ShouldJoinActivity()
        {
            TestUtils.InsertTestMainActivity(true);
            TestUtils.InsertTestEmployeeActivity();
            var activityManager = new ActivityManager(new ActivityRepository(), new UserRepository());

            activityManager.JoinActivity(TestUtils.TestStaffUserId, TestUtils.TestEmployeeActivityId);
            var participants = activityManager.GetParticipants(TestUtils.TestEmployeeActivityId);

            Assert.AreEqual(1, participants.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            var repo = new TestRepository();
            repo.Cleanup();
        }
    }
}
