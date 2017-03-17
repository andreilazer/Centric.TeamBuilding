using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Centric.TeamBuilding.Entities;
using Dapper;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public class DayRepository
    {
        public IEnumerable<Day> GetDays()
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[Day]";

                connection.Open();
                return connection.Query<Day>(sql);
            }
        }

        public void CreateDay(Day day)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Day] (Date, Description)
                          VALUES (@Date, @Description)";

                connection.Open();
                connection.Execute(sql, day);
            }
        }
    }
}