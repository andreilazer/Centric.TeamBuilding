using System;

namespace Centric.TeamBuilding.BussinesLogic.Utils
{
    public static class DateExtensions
    {
        public static DateTime RemoveTimeInformation(this DateTime date)
        {
            date.AddHours(-date.Hour);
            date.AddMinutes(-date.Minute);
            date.AddSeconds(-date.Second);
            return date;
        }
    }
}
