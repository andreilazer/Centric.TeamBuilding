using System;
using System.Collections.Generic;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;
using System.ComponentModel.DataAnnotations;
using Centric.TeamBuilding.BussinesLogic.Validators;

namespace Centric.TeamBuilding.BussinesLogic.Managers
{
    public class ActivityManager
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserRepository _userRepository;

        public ActivityManager(IActivityRepository activityRepository, IUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _userRepository = userRepository;
        }

        public void CreateMainActivity(MainActivity activity)
        {
            var activityValidator = new ActivityValidator(_activityRepository);
            var activityValidationResult = activityValidator.ValidateMainActivity(activity);

            if (activityValidationResult.IsValid) {
                var creatorRole = _userRepository.GetUser(activity.CreatorId).Role;
                if (creatorRole != UserRoles.Staff)
                {
                    throw new UnauthorizedAccessException("Only Staff users can create main activities!");
                }

                _activityRepository.CreateMainActivity(activity);
            }
            else
            {
                throw new ValidationException(activityValidationResult.Message);
            }
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
