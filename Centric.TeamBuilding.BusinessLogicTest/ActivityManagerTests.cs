using System;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Centric.TeamBuilding.BussinesLogic.Managers;
using NSubstitute;
using Centric.TeamBuilding.DataAccess.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Centric.TeamBuilding.BusinessLogicTest
{
    [TestClass]
    public class ActivityManagerTests
    {
        private Guid mainActivityCreatorId = Guid.NewGuid();
        private Guid mainActivityDayId = Guid.NewGuid();

        [TestMethod]
        public void CreateMainActivity_ShouldThrowValidationException_WhenStartTimeIsAfterEndTime()
        {
            try
            {
                var activityRepositoryStub = Substitute.For<IActivityRepository>();
                var userRepositoryStub = Substitute.For<IUserRepository>();
                var activityManager = new ActivityManager(activityRepositoryStub);
                var newActivity = GetDummyNewMainActivity();
                newActivity.StartTime = newActivity.EndTime.AddHours(1);

                activityManager.CreateMainActivity(newActivity);
                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("End time must be after start time",e.Message);
            }
        }

        [TestMethod]
        public void CreateMainActivity_ShouldThrowValidationException_WhenIntervalCollidesWithExistingActivities()
        [ExpectedException(typeof(ValidationException))]
        public void CreateMainActivity_ShouldThrowValidationException_WhenNewActivityEndTimeAfterNextActivityStartTime()
        {

            var activityRepositoryStub = Substitute.For<IActivityRepository>();
            var userRepositoryStub = Substitute.For<IUserRepository>();
            var activityManager = new ActivityManager(activityRepositoryStub, userRepositoryStub);

            userRepositoryStub.GetUser(Arg.Any<Guid>()).Returns(GetDummyUser());

            var start = new DateTime(2017, 01, 01, 13, 30, 00);
            var end = new DateTime(2017, 01, 01, 14, 30, 00);
            var newActivity = GetDummyNewMainActivity(start, end);

            activityRepositoryStub.GetMainActivities(Arg.Any<Guid>()).Returns(GetDummyMainActivities());

            activityManager.CreateMainActivity(newActivity);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateMainActivity_ShouldThrowValidationException_WhenNewActivityStartTimeBeforePreviousActivityEndTime()
        {

            var activityRepositoryStub = Substitute.For<IActivityRepository>();
            var userRepositoryStub = Substitute.For<IUserRepository>();
            var activityManager = new ActivityManager(activityRepositoryStub, userRepositoryStub);

            userRepositoryStub.GetUser(Arg.Any<Guid>()).Returns(GetDummyUser());

            var start = new DateTime(2017, 01, 01, 12, 30, 00);
            var end = new DateTime(2017, 01, 01, 13, 30, 00);
            var newActivity = GetDummyNewMainActivity(start, end);

            activityRepositoryStub.GetMainActivities(Arg.Any<Guid>()).Returns(GetDummyMainActivities());

            activityManager.CreateMainActivity(newActivity);
        }

        private User GetDummyUser()
        {
            return new User
            {
                Role = UserRoles.Staff
            };
        }

        private MainActivity GetDummyNewMainActivity(DateTime startTime, DateTime endTime)
        {
            return new MainActivity()
            {
                Title = "Activity title",
                Location = "Activity location",
                Description = "Activity description",
                IsFreeActivity = false,
                DayId = mainActivityDayId,
                CreatorId = mainActivityCreatorId,
                StartTime = startTime,
                EndTime = endTime,
            };
        }

        private List<MainActivity> GetDummyMainActivities()
        {
            return new List<MainActivity>
            {
                new MainActivity
                {
                    Id = Guid.NewGuid(),
                    Location = "Test location",
                    Description = "Test description",
                    Title = "Test title",
                    CreatorId = mainActivityCreatorId,
                    DayId = Guid.NewGuid(),
                    IsFreeActivity = false,
                    StartTime = new DateTime(2017, 01, 01, 12, 00, 00),
                    EndTime = new DateTime(2017, 01, 01, 13, 00, 00)
                },
                new MainActivity
                {
                    Id = Guid.NewGuid(),
                    Location = "Test location 1",
                    Description = "Test description 1",
                    Title = "Test title 1",
                    CreatorId = mainActivityCreatorId,
                    DayId = Guid.NewGuid(),
                    IsFreeActivity = false,
                    StartTime = new DateTime(2017, 01, 01, 14, 00, 00),
                    EndTime = new DateTime(2017, 01, 01, 16, 30, 00)
                }
            };
        }
    }
}
