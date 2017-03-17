using System;
using System.Linq;
using System.Security.Authentication;
using Centric.TeamBuilding.BussinesLogic.Managers;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using Centric.TeamBuilding.Tests.Repository;
using Centric.TeamBuilding.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Centric.TeamBuilding.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private const string TestPassword = "literatură";
        private const string TestEmail = "calistrat@centric.eu";

        [TestMethod]
        public void ShouldRegisterUser()
        {
            var userManager = new UserManager(new UserRepository());
            var testRepository = new TestRepository();
            var newUser = new User
            {
                FirstName = "Calistrat",
                LastName = "Hogaș",
                Password = TestPassword,
                BirthDate = new DateTime(2000, 01, 01),
                Email = TestEmail,
                Gender = Gender.Male,
                Role = UserRoles.Employee
            };
            userManager.Register(newUser);

            var registeredUser = testRepository.GetAllUsers().First();

            Assert.IsNull(newUser.PhoneNumber, registeredUser.PhoneNumber);
            Assert.AreEqual(newUser.Email, registeredUser.Email);
            Assert.AreEqual(newUser.FirstName, registeredUser.FirstName);
        }

        [TestMethod]
        public void ShouldLoginUser()
        {
            var userManager = new UserManager(new UserRepository());
            TestUtils.InsertTestStaffUser();

            var user = userManager.Login(TestUtils.TestStaffEmail, TestUtils.TestPassword);
            Assert.AreEqual(TestUtils.TestStaffUserId, user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void ShouldNotLoginUserAndThrowException_WhenInvalidPassword()
        {
            var userManager = new UserManager(new UserRepository());
            TestUtils.InsertTestStaffUser();

            userManager.Login(TestUtils.TestEmployeeEmail, "matematică");
        }

        [TestCleanup]
        public void Cleanup()
        {
            var repo = new TestRepository();
            repo.Cleanup();
        }
    }
}
