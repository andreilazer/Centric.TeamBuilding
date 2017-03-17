using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Centric.TeamBuilding.BussinesLogic.Utils;
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
            var user = _userRepository.GetUser(email);
            if (user == null)
            {
                throw new AuthenticationException("User not found");
            }
            if (!user.Password.Equals(password.HashStringMd5()))
            {
                throw new AuthenticationException("Incorrect password");
            }
            return user;
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
                user.Password = user.Password.HashStringMd5();
                _userRepository.InsertUser(user);
            }
            else
            {
                throw new ValidationException(userValidationResult.Message);
            }
        }
    }
}
