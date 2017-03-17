using System;

namespace Centric.TeamBuilding.BussinesLogic.Entities
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
    }
}
