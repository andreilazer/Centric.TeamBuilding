using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Validators
{
    public class UserValidator
    {
        private IUserRepository _userRepository;

        public UserValidator(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public ValidationResult Validate(User user)
        {
            var result = user.Validate();

            if (result.IsValid)
            {
                var existingUser = _userRepository.GetUser(user.Email);
                if (existingUser != null)
                {
                    result.IsValid = false;
                    result.Message = "Email already registered";
                }
            }
            return result;
        }
    }
}
