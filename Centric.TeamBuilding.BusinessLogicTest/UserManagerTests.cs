using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.BussinesLogic.Utils;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

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
                var userManager = new UserManager(new UserRepository());

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
                var user = GetDummyValidUser();
                user.Email = "email@google.ro";
                var userManager = new UserManager(new UserRepository());

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
                var user = GetDummyValidUser();
                user.Password = "123";
                var userManager = new UserManager(new UserRepository());

                userManager.Register(user);

                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Password too short", e.Message);
            }
        }

        [TestMethod]
        public void RegisterUser_ShouldNotThrowValidationException_WhenUserAlreadyExistsWithEmail()
        {
            try
            {
                var inputUser = GetDummyValidUser();
                var insertedUser = GetDummyValidUser();
                var repositoryStub = Substitute.For<IUserRepository>();
                repositoryStub.GetUser(inputUser.Email).Returns(insertedUser);
                var userManager = new UserManager(repositoryStub);

                userManager.Register(inputUser);
                Assert.IsTrue(false, "Should throw validation error");
            }
            catch (ValidationException e)
            {
                Assert.AreEqual("Email already registered", e.Message);                
            }
        }

        [TestMethod]
        public void RegisterUser_ShouldNotThrowAnyExceptions_WhenInputValidUser()
        {
            var inputUser = GetDummyValidUser();
            var repositoryStub = Substitute.For<IUserRepository>();

            var userManager = new UserManager(repositoryStub);

            userManager.Register(inputUser);

            Assert.IsTrue(true, "Everything went well");
        }

        [TestMethod]
        public void Login_ShouldThrowAuthenticationException_WhenEmailNotFoundInRepository()
        {
            try
            {
                var email = "email@centric.eu";
                var password = "123456";
                var repositoryStub = Substitute.For<IUserRepository>();
                repositoryStub.GetUser(Arg.Any<string>()).Returns((User) null);

                var userManager = new UserManager(repositoryStub);
                userManager.Login(email, password);

                Assert.IsTrue(false, "Should throw authentication error");
            }
            catch (AuthenticationException e)
            {
                Assert.AreEqual("User not found", e.Message);
            }
        }

        [TestMethod]
        public void Login_ShouldThrowAuthenticationException_WhenPasswordIsIncorrect()
        {
            try
            {
                var email = "email@centric.eu";
                var password = "123456";
                var repositoryStub = Substitute.For<IUserRepository>();
                var existingUser = new User {Password = "OtherPassword"};
                repositoryStub.GetUser(Arg.Any<string>()).Returns(existingUser);

                var userManager = new UserManager(repositoryStub);
                userManager.Login(email, password);

                Assert.IsTrue(false, "Should throw authentication error");
            }
            catch (AuthenticationException e)
            {
                Assert.AreEqual("Incorrect password", e.Message);
            }
        }

        [TestMethod]
        public void LoginUser_ShouldNotThrowAnyExceptions_WhenValidCredentials()
        {
            var email = "email@centric.eu";
            var password = "123456";
            var repositoryStub = Substitute.For<IUserRepository>();
            var existingUser = new User { Password = password.HashStringMd5() };
            repositoryStub.GetUser(email).Returns(existingUser);

            var userManager = new UserManager(repositoryStub);

            userManager.Login(email, password);

            Assert.IsTrue(true, "Everything went well");
        }

        private static User GetDummyValidUser()
        {
            return new User
            {
                Password = "1234567",
                Email = "email@centric.eu",
                FirstName = "Ion",
                LastName = "Creanga",
                BirthDate = DateTime.Now,
                Gender = Gender.Male
            };
        }
    }
}
