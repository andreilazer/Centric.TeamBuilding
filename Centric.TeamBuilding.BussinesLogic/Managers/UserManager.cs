using System.ComponentModel.DataAnnotations;
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
            var userValidationResult = user.Validate();

            if (userValidationResult.IsValid)
            {
                var existingUser = _userRepository.GetUser(user.Email);
                if (existingUser != null)
                {
                    throw new ValidationException("Email already registered");
                }
                _userRepository.InsertUser(user);
            }
            else
            {
                throw new ValidationException(userValidationResult.Message);
            }
        }
    }
}
