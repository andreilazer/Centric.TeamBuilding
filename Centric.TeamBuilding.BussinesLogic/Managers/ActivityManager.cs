using System;
using System.Collections.Generic;
using Centric.TeamBuilding.BussinesLogic.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Domain
{
    public class ActivityManager
    {
        public MainActivity CreateMainActivity(MainActivity activity)
        {
            return new MainActivity();
        }

        public EmployeeActivity CreateEmployeeActivity(EmployeeActivity activity)
        {
            return new EmployeeActivity();
        }

        public IEnumerable<MainActivity> GetDayActivities(Guid dayId)
        {
            return new List<MainActivity>();
        }

        public IEnumerable<EmployeeActivity> GetEmployeeActivities(Guid mainActivityId)
        {
            return new List<EmployeeActivity>();
        }

        public void JoinActivity(Guid userId, Guid activityId)
        {
            
        }

        public IEnumerable<User> GetParticipants(Guid activityId)
        {
            return new List<User>();
        }
    }
}
