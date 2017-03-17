using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centric.TeamBuilding.DataAccess.Repositories;
using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.BussinesLogic.Validators
{
    public class ActivityValidator
    {
        private IActivityRepository _activityRepository;

        public ActivityValidator(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public ValidationResult ValidateMainActivity(MainActivity activity)
        {
            var validationResult = activity.Validate();

            if (validationResult.IsValid)
            {
                var allMainActivities = _activityRepository.GetMainActivities(activity.DayId);

                if (!IsValidActivityInterval(allMainActivities, activity))
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "Activity interval collides another existing activity!";
                }
            }
            return validationResult;
        }

        public ValidationResult ValidateEmployeeActivity(EmployeeActivity activity)
        {
            var validationResult = activity.Validate();
            if (validationResult.IsValid)
            {
                var parentActivity = _activityRepository.GetActivity(activity.ParentId);
                if (parentActivity == null)
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "Invalid parent activity.";
                }
                var allActivities = _activityRepository.GetEmployeeActivities(activity.ParentId);
                if (!IsValidActivityInterval(allActivities, activity))
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "Activity interval collides another existing activity!";
                }

            }
            return validationResult;
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
    }
}
