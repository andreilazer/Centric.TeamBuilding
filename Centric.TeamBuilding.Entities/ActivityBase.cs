using System;

namespace Centric.TeamBuilding.Entities
{
    public abstract class ActivityBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Location { get; set; }

        public Guid CreatorId { get; set; }

        public Guid DayId { get; set; }

        public virtual ValidationResult Validate()
        {
            var result = new ValidationResult() { IsValid = false };
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Location))
            {
                result.Message = "Fill all required fields";
                return result;
            }
            if (StartTime > EndTime)
            {
                result.Message = "End time must be after start time";
                return result;
            }
            result.IsValid = true;
            return result;
        }
    }
}
