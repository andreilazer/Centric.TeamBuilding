using System;
using System.Collections.Generic;

namespace Centric.TeamBuilding.BussinesLogic.Entities
{
    public class EmployeeActivity
    {
        public Guid ParentActivityId { get; set; }

        public List<Guid> Participants { get; set; }
    }
}