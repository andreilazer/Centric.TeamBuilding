using System;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.BussinesLogic.Utils;
using Centric.TeamBuilding.Entities;
using Centric.TeamBuilding.Tests.Repository;

namespace Centric.TeamBuilding.Tests.Utils
{
    public static class TestUtils
    {
        public const string TestDescription = "inserted from test";
        public const string TestPassword = "literatură";
        public const string TestEmployeeEmail = "calistrat@centric.eu";
        public const string TestStaffEmail = "staff@centric.eu";
        public static Guid TestStaffUserId = new Guid("a7ff14cd-4b03-40ea-ad5b-5029baf0e179");
        public static Guid TestEmployeeUserId = new Guid("1b677ec1-d286-45b9-9058-7a5d22df4de9");
        public static Guid TestDayId = new Guid("399d1d7e-511f-424a-a9f7-2a2f4a4b877f");
        public static Guid TestMainActivityId = new Guid("62406bfc-b940-42b6-b1f7-4de05d8ce133");
        public static Guid TestEmployeeActivityId = new Guid("e19445fa-ae87-43ce-b1d5-f14c008c8f7f");

        public static void InsertTestDay()
        {
            var testRepository = new TestRepository();
            var day = new Day
            {
                Id = TestDayId,
                Date = new DateTime(2017, 12, 31),
                Description = TestDescription
            };
            testRepository.InsertTestDay(day);
        }

        public static void InsertTestStaffUser()
        {
            var testRepository = new TestRepository();
            var newUser = new User
            {
                Id = TestStaffUserId,
                FirstName = "Calistrat",
                LastName = "Hogaș",
                Password = TestPassword.HashStringMd5(),
                BirthDate = new DateTime(2000, 01, 01),
                Email = TestStaffEmail,
                Gender = Gender.Male,
                Role = UserRoles.Staff
            };
            testRepository.InsertTestUser(newUser);
        }

        public static void InsertTestEmployeeUser()
        {
            var testRepository = new TestRepository();
            var newUser = new User
            {
                Id = TestEmployeeUserId,
                FirstName = "Calistrat",
                LastName = "Hogaș",
                Password = TestPassword,
                BirthDate = new DateTime(2000, 01, 01),
                Email = TestEmployeeEmail,
                Gender = Gender.Male,
                Role = UserRoles.Employee
            };
            testRepository.InsertTestUser(newUser);
        }

        public static void InsertTestMainActivity(bool isFreeActivity = false)
        {
            var testRepository = new TestRepository();
            var newActivity = new MainActivity()
            {
                Id = TestMainActivityId,
                Description = TestUtils.TestDescription,
                CreatorId = TestUtils.TestStaffUserId,
                EndTime = DateTime.Now.AddMinutes(30),
                StartTime = DateTime.Now,
                IsFreeActivity = isFreeActivity,
                Location = "u",
                DayId = TestUtils.TestDayId,
                Title = "Test activity"
            };
            testRepository.InsertTestMainActivity(newActivity);
        }

        public static void InsertTestEmployeeActivity()
        {
            var testRepository = new TestRepository();
            var newActivity = new EmployeeActivity()
            {
                Id = TestEmployeeActivityId,
                Description = TestUtils.TestDescription,
                CreatorId = TestUtils.TestEmployeeUserId,
                EndTime = DateTime.Now.AddMinutes(30),
                StartTime = DateTime.Now,
                Location = "u",
                DayId = TestUtils.TestDayId,
                Title = "Test activity",
                ParentId = TestMainActivityId
            };
            testRepository.InsertTestEmployeeActivity(newActivity);
        }
    }
}