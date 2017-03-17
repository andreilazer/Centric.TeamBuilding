using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class DayManager
    {
        private readonly DayRepository _dayRepository;

        public DayManager()
        {
            _dayRepository = new DayRepository();
        }
        public void Create(Day day)
        {

            _dayRepository.CreateDay(day);
        }

        public IEnumerable<Day> GetDays()
        {
            return _dayRepository.GetDays();
        }
    }
}
