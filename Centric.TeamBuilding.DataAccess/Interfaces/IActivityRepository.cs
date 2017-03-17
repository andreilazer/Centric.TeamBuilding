using System;
using System.Collections.Generic;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public interface IActivityRepository
    {
        void AddUserToActivity(Guid userId, Guid activityId);
        void CreateEmployeeActivity(EmployeeActivity activity);
        void CreateMainActivity(MainActivity activity);
        IEnumerable<User> GetActivityParticipants(Guid activityId);
        IEnumerable<MainActivity> GetDayActivities(Guid dayId);
        IEnumerable<EmployeeActivity> GetEmployeeActivities(Guid mainActivityId);
    }
}