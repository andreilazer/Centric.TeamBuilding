using System.Configuration;
using System.Data.Common;

namespace Centric.TeamBuilding.DataAccess
{
    public static class Constants
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["CentricTeamBuilding"].ConnectionString;
    }
}