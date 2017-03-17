using System;
using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class ActivityManager
    {
        private ActivityRepository _activityRepository;

        public ActivityManager()
        {
            _activityRepository = new ActivityRepository();
        }

        public void CreateMainActivity(MainActivity activity)
        {
            _activityRepository.CreateMainActivity(activity);
        }

        public void CreateEmployeeActivity(EmployeeActivity activity)
        {
            _activityRepository.CreateEmployeeActivity(activity);
        }

        public IEnumerable<MainActivity> GetDayActivities(Guid dayId)
        {
            return _activityRepository.GetDayActivities(dayId);
        }

        public IEnumerable<EmployeeActivity> GetEmployeeActivities(Guid mainActivityId)
        {
            return _activityRepository.GetEmployeeActivities(mainActivityId);
        }

        public void JoinActivity(Guid userId, Guid activityId)
        {
            _activityRepository.AddUserToActivity(userId, activityId);
        }

        public IEnumerable<User> GetParticipants(Guid activityId)
        {
            return _activityRepository.GetActivityParticipants(activityId);
        }
    }
}
