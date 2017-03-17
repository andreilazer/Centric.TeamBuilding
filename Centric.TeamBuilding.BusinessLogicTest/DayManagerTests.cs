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

        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenProvidingEmptyDescription()
        {
            var day = new Day
            {
                Date = DateTime.UtcNow
            };

            var dayRepository = new DayRepository();
            var dayManager = new DayManager(dayRepository);
            dayManager.Create(day);
        }

        [TestMethod]
        public void Create_ShouldThrowValidationException_WhenProvidingDateThatAlreadyExists()
        {
            try
            {
                var dayRepository = Substitute.For<IDayRepository>();
                var existingDays = new List<Day>{
                new Day
                    {
                        Date = DateWithoutHours
                    }
            };
                dayRepository.GetDays().Returns(existingDays);

                var dayManager = new DayManager(dayRepository);
                var day = new Day
                {
                    Date = DateWithoutHours,
                    Description = "Test Description"
                };
                dayManager.Create(day);

                Assert.IsTrue(false, "Should not get here!");
            }
            catch(ValidationException ex)
            {
                Assert.AreEqual("Day already registered!", ex.Message);
            }
        }
    }
}
