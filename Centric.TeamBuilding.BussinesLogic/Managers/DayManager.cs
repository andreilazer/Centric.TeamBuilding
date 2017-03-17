using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Centric.TeamBuilding.BussinesLogic.Utils;
using Centric.TeamBuilding.BussinesLogic.Validators;
using System;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class DayManager
    {
        private readonly IDayRepository _dayRepository;
        private readonly IUserRepository _userRepository;

        public DayManager(IDayRepository dayRepository, IUserRepository userRepository)
        {
            _dayRepository = dayRepository;
            _userRepository = userRepository;
        }

        public void Create(Day day, Guid creatorId)
        {
            day.Date = DateExtensions.RemoveTimeInformation(day.Date);

            var dayValidator = new DayValidator(_dayRepository, _userRepository);
            var dayValidationResult = dayValidator.Validate(day, creatorId);

            if (dayValidationResult.IsValid)
            {
                _dayRepository.CreateDay(day);
            }
            else
            {
                throw new ValidationException(dayValidationResult.Message);
            }
        }

        public IEnumerable<Day> GetDays()
        {
            return _dayRepository.GetDays();
        }
    }
}
