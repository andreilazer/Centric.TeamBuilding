using System;
using System.ComponentModel.DataAnnotations;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Centric.TeamBuilding.BusinessLogicTest
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void RegisterUser_ShouldThrowValidationException_WhenEmptyRequiredFields()
        {
            try
            {
                var user = new User();
                var userManager = new UserManager();

                userManager.Register(user);
                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Fill all required fields", e.Message);
            }

        }

        [TestMethod]
        public void RegisterUser_ShouldThrowValidationException_WhenEmailIsNotCentricEu()
        {
            try
            {
                var user = GetDummyUser();
                var userManager = new UserManager();

                userManager.Register(user);
                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Invalid Email Address", e.Message);
            }
        }

        [TestMethod]
        public void RegisterUser_ShouldThrowValidationException_WhenPasswordIsThreeCharacters()
        {
            try
            {
                var user = GetDummyUser();
                user.Email = "user@centric.eu";
                user.Password = "123";
                var userManager = new UserManager();

                userManager.Register(user);

                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Password too short", e.Message);
            }
        }
               
        private static User GetDummyUser()
        {
            return new User
            {
                Password = "1234567",
                Email = "email@google.ro",
                FirstName = "Ion",
                LastName = "Creanga",
                BirthDate = DateTime.Now,
                Gender = Gender.Male
            };
        }
    }
}
