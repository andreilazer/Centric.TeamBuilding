using System.Collections.Generic;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public interface IDayRepository
    {
        void CreateDay(Day day);
        IEnumerable<Day> GetDays();
    }
}