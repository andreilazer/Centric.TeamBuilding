using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Centric.TeamBuilding.BussinesLogic.Utils;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class DayManager
    {
        private readonly IDayRepository _dayRepository;

        public DayManager(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public void Create(Day day)
        {
            day.Date = DateExtensions.RemoveTimeInformation(day.Date);

            var dayValidationResult = day.Validate();
            if (dayValidationResult.IsValid)
            {
                var isDuplicateDay = _dayRepository.GetDays().Any(d=>d.Date == day.Date);
                if (isDuplicateDay)
                {
                    throw new ValidationException("Day already registered!");
                }

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
