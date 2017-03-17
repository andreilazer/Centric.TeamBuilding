using System;
using System.Collections.Generic;

namespace Centric.TeamBuilding.Entities
{
    public class EmployeeActivity : ActivityBase
    {
        public Guid ParentId { get; set; }

        public List<Guid> Participants { get; set; }
    }
}