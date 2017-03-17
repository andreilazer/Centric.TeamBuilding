using System;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Centric.TeamBuilding.BussinesLogic.Managers;
using System.ComponentModel.DataAnnotations;
using Centric.TeamBuilding.DataAccess.Repositories;
using NSubstitute;
using System.Collections.Generic;
using Centric.TeamBuilding.BussinesLogic.Utils;

namespace Centric.TeamBuilding.BusinessLogicTest
{
    [TestClass]
    public class DayManagerTests
    {
        private DateTime DateWithoutHours = new DateTime(2016, 01, 01);
        private Guid DummyStaffUserId = new Guid("1b0ee50c-6822-4996-b323-14900b1037d5");

        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenProvidingEmptyDescription()
        {
            try
            {
                var dayRepository = Substitute.For<IDayRepository>();
                var userRepository = Substitute.For<IUserRepository>();
                var day = new Day
                {
                    Date = DateTime.UtcNow
                };

                var dayManager = new DayManager(dayRepository, userRepository);
                dayManager.Create(day, DummyStaffUserId);
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("Please enter a day description!", ex.Message);
            }
        }

        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenProvidingDateThatAlreadyExists()
        {
            try
            {
                var dayRepository = Substitute.For<IDayRepository>();
                var userRepository = Substitute.For<IUserRepository>();
                var existingDays = new List<Day>{
                new Day
                    {
                        Date = DateWithoutHours
                    }
                };
                dayRepository.GetDays().Returns(existingDays);
                userRepository.GetUser(DummyStaffUserId).Returns(GetDummyStaffUser());

                var dayManager = new DayManager(dayRepository, userRepository);
                var day = new Day
                {
                    Date = DateWithoutHours,
                    Description = "Test Description"
                };
                dayManager.Create(day, DummyStaffUserId);

                Assert.IsTrue(false, "Should not get here!");
            }
            catch(ValidationException ex)
            {
                Assert.AreEqual("Day already registered!", ex.Message);
            }
        }

        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenUserNotPartOfStaff()
        {
            try
            {
                var dayRepository = Substitute.For<IDayRepository>();
                var userRepository = Substitute.For<IUserRepository>();
                var existingDays = new List<Day>{
                new Day
                    {
                        Date = DateWithoutHours.AddDays(1)
                    }
                };
                dayRepository.GetDays().Returns(existingDays);
                userRepository.GetUser(DummyStaffUserId).Returns(GetDummyEmployeeUser());

                var dayManager = new DayManager(dayRepository, userRepository);
                var day = new Day
                {
                    Date = DateWithoutHours,
                    Description = "Test Description"
                };
                dayManager.Create(day, DummyStaffUserId);

                Assert.IsTrue(false, "Should not get here!");
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("Only staff members can create days!", ex.Message);
            }
        }

        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenUserDoesNotExist()
        {
            try
            {
                var dayRepository = Substitute.For<IDayRepository>();
                var userRepository = Substitute.For<IUserRepository>();
                var existingDays = new List<Day>{
                new Day
                    {
                        Date = DateWithoutHours.AddDays(1)
                    }
                };
                dayRepository.GetDays().Returns(existingDays);
                userRepository.GetUser(DummyStaffUserId).Returns((User)null);

                var dayManager = new DayManager(dayRepository, userRepository);
                var day = new Day
                {
                    Date = DateWithoutHours,
                    Description = "Test Description"
                };
                dayManager.Create(day, DummyStaffUserId);

                Assert.IsTrue(false, "Should not get here!");
            }
            catch (ValidationException ex)
            {
                Assert.AreEqual("User does not exist!", ex.Message);
            }
        }

        private User GetDummyStaffUser()
        {
            return new User
            {
                Role = UserRoles.Staff,
                Id = DummyStaffUserId
            };
        }

        private User GetDummyEmployeeUser()
        {
            return new User
            {
                Role = UserRoles.Employee,
                Id = DummyStaffUserId
            };
        }
    }
}
