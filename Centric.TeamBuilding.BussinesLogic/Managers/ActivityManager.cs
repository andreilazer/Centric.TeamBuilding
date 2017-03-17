using System;
using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void CreateMainActivity(MainActivity activity)
        {
            var activityValidationResult = activity.Validate();

            if (activityValidationResult.IsValid)
            {
                var creatorRole = new UserRepository().GetUser(activity.CreatorId).Role;

                if (creatorRole != UserRoles.Staff)
                {
                    throw new UnauthorizedAccessException("Only Staff users can create main activities!");
                }
                var allMainActivities = new ActivityRepository().GetMainActivities(activity.DayId);

                var isValidActivityInterval = IsValidActivityInterval(allMainActivities, activity);
                if (!isValidActivityInterval)
                {
                    throw new ValidationException("Activity interval collides another existing activity!");
                }

                _activityRepository.CreateMainActivity(activity);
            }
            else
            {
                throw new ValidationException(activityValidationResult.Message);
            }
        }

        private bool IsValidActivityInterval(IEnumerable<ActivityBase> allActivities, ActivityBase newActivity)
        {
            if (!allActivities.Any())
            {
                return true;
            }
            allActivities = allActivities.OrderBy(a => a.StartTime);
            var nextActivityIndex = allActivities.TakeWhile(a => a.StartTime < newActivity.StartTime).Count();
            if(nextActivityIndex == null || nextActivityIndex == 0)
            {
                return true;
            }
            var previousActivityEndTime = allActivities.ElementAt(nextActivityIndex - 1).EndTime;
            if(previousActivityEndTime > newActivity.StartTime)
            {
                return false;
            }

            return true;
        }

        public void CreateEmployeeActivity(EmployeeActivity activity)
        {
            _activityRepository.CreateEmployeeActivity(activity);
        }

        public IEnumerable<MainActivity> GetDayActivities(Guid dayId)
        {
            return _activityRepository.GetMainActivities(dayId);
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
