using System;
using System.Collections.Generic;

namespace Centric.TeamBuilding.Entities
{
    public class EmployeeActivity : ActivityBase
    {
        public Guid ParentId { get; set; }

        public List<Guid> Participants { get; set; }

        public override ValidationResult Validate()
        {
            var result = base.Validate();
            if (ParentId == Guid.Empty)
            {
                result.IsValid = false;
                result.Message = "Parent activity not specified";
            }
            return result;
        }
    }
}