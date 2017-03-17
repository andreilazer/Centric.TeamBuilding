using System;

namespace Centric.TeamBuilding.Entities
{
    public class Day
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public ValidationResult Validate()
        {
            var result = new ValidationResult() { IsValid = false };
            if (string.IsNullOrEmpty(Description))
            {
                result.Message = "Please enter a day description!";
                return result;
            }
            result.IsValid = true;
            return result;
        }
    }
}
