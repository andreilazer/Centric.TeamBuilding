using System;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Centric.TeamBuilding.BussinesLogic.Managers;
using NSubstitute;
using Centric.TeamBuilding.DataAccess.Repositories;
using System.Collections.Generic;

namespace Centric.TeamBuilding.BusinessLogicTest
{
    [TestClass]
    public class ActivityManagerTests
    {
        private Guid mainActivityCreatorId = Guid.NewGuid();
        private Guid mainActivityDayId = Guid.NewGuid();

        [TestMethod]
        public void CreateMainActivity_ShouldThrowValidationException_WhenIntervalCollidesWithExistingActivities()
        {

            var activityRepositoryStub = Substitute.For<IActivityRepository>();
            var userRepositoryStub = Substitute.For<IUserRepository>();
            var activityManager = new ActivityManager(activityRepositoryStub);

            userRepositoryStub.GetUser(Arg.Any<Guid>()).Returns(new User
            {
                Role = UserRoles.Staff
            });

            var newActivity = GetDummyNewMainActivity();

            activityRepositoryStub.GetMainActivities(Arg.Any<Guid>()).Returns(GetDummyMainActivities());
        }

        private object GetDummyNewMainActivity()
        {
            return new MainActivity()
            {
                Title = "Activity title",
                Location = "Activity location",
                Description = "Activity description",
                IsFreeActivity = false,
                DayId = mainActivityDayId,
                CreatorId = mainActivityCreatorId,
                StartTime = new DateTime(2017, 01, 01, 13, 00, 00),
                EndTime = new DateTime(2017, 01, 01, 13, 30, 00),
            };
        }

        private List<MainActivity> GetDummyMainActivities()
        {
            return new List<MainActivity>
            {
                new MainActivity
                {
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
                    Location = "Test location 1",
                    Description = "Test description 1",
                    Title = "Test title 1",
                    CreatorId = mainActivityCreatorId,
                    DayId = Guid.NewGuid(),
                    IsFreeActivity = false,
                    StartTime = new DateTime(2017, 01, 01, 14, 00, 00),
                    EndTime = new DateTime(2017, 01, 01, 16, 30, 00)
                },
                new MainActivity
                {
                    Location = "Test location 2",
                    Description = "Test description 2",
                    Title = "Test title 2",
                    CreatorId = mainActivityCreatorId,
                    DayId = Guid.NewGuid(),
                    IsFreeActivity = false,
                    StartTime = new DateTime(2017, 01, 01, 17, 00, 00),
                    EndTime = new DateTime(2017, 01, 01, 18, 30, 00)
                }
            };
        }
    }
}
