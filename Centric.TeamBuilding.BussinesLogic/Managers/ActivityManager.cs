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
        private IUserRepository _userRepository;

        public ActivityManager(IActivityRepository activityRepository, IUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
        }

        public void CreateMainActivity(MainActivity activity)
        {
            var activityValidationResult = activity.Validate();

            if (activityValidationResult.IsValid) {
                var creatorRole = _userRepository.GetUser(activity.CreatorId).Role;

                if (creatorRole != UserRoles.Staff)
                {
                    throw new UnauthorizedAccessException("Only Staff users can create main activities!");
                }
                var allMainActivities = _activityRepository.GetMainActivities(activity.DayId);

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
            if (allActivities.Any(a => a.StartTime <= newActivity.StartTime && a.EndTime >= newActivity.StartTime)
                || allActivities.Any(a => a.StartTime <= newActivity.EndTime && a.EndTime >= newActivity.EndTime))
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
