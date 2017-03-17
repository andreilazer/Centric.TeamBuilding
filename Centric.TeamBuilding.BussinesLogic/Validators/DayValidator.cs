using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using System;
using System.Linq;

namespace Centric.TeamBuilding.BussinesLogic.Validators
{
    public class DayValidator
    {
        private IDayRepository _dayRepository;
        private IUserRepository _userRepository;

        public DayValidator(IDayRepository dayRepository, IUserRepository userRepository)
        {
            _dayRepository = dayRepository;
            _userRepository = userRepository;
        }

        public ValidationResult Validate(Day day, Guid creatorId)
        {
            var result = day.Validate();
            if (result.IsValid)
            {
                var isDuplicateDay = _dayRepository.GetDays().Any(d => d.Date == day.Date);
                if (isDuplicateDay)
                {
                    result.IsValid = false;
                    result.Message = "Day already registered!";
                    return result;
                }

                var creatorRole = _userRepository.GetUser(creatorId)?.Role;

                if (creatorRole == null)
                {
                    result.IsValid = false;
                    result.Message = "User does not exist!";
                    return result;
                }

                if (creatorRole != UserRoles.Staff)
                {
                    result.IsValid = false;
                    result.Message = "Only staff members can create days!";
                    return result;
                }
            }
            return result;
        }
    }
}
