using System;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager()
        {
            _userRepository = new UserRepository();
        }

        public User Login(string email, string password)
        {
            return _userRepository.GetUser(email);
        }

        public void Register(User user)
        {
            _userRepository.InsertUser(user);
        }
    }
}
