using System.Collections.Generic;
using Centric.TeamBuilding.BussinesLogic.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class DayManager
    {
        public Day Create(Day day)
        {
            return new Day();
        }

        public IEnumerable<Day> GetDays()
        {
            return new List<Day>();
        }
    }
}
