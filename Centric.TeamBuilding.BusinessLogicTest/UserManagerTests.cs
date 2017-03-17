using System;
using Centric.TeamBuilding.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Centric.TeamBuilding.BusinessLogicTest
{
    [TestClass]
    public class UserManagerTests
    {
        [TestMethod]
        public void RegisterUser_ShouldThrowInvalidException_WhenEmptyRequiredFields()
        {
            var user = new User
            {

            };
        }
    }
}
