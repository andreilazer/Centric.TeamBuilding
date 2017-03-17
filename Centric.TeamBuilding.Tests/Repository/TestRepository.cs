using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Centric.TeamBuilding.Entities;
using Dapper;

namespace Centric.TeamBuilding.Tests.Repository
{
    public class TestRepository
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["CentricTeamBuilding"].ConnectionString;

        public void Cleanup()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"DELETE FROM [dbo].[ActivityUsers]
                            DELETE FROM [dbo].[Activity]
                            DELETE FROM [dbo].[User]
                            DELETE FROM [dbo].[Day]";

                connection.Open();
                connection.Execute(sql);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[User]";

                connection.Open();
                return connection.Query<User>(sql);
            }
        }

        public IEnumerable<Day> GetAllDays()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[Day]";

                connection.Open();
                return connection.Query<Day>(sql);
            }
        }

        public IEnumerable<MainActivity> GetAllActivities()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[Activity]";

                connection.Open();
                return connection.Query<MainActivity>(sql);
            }
        }

        public void InsertTestUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[User]
                          (Id, FirstName, LastName, Email, Password, Role, Gender, PhoneNumber, BirthDate)
                          VALUES (@Id, @FirstName, @LastName, @Email, @Password, @Role, @Gender, @PhoneNumber, @BirthDate)";

                connection.Open();
                connection.Execute(sql, user);
            }
        }

        public void InsertTestDay(Day day)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Day] (Id, Date, Description)
                          VALUES (@Id, @Date, @Description)";

                connection.Open();
                connection.Execute(sql, day);
            }
        }

        public void InsertTestEmployeeActivity(EmployeeActivity newActivity)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Activity] 
                            (Id, Title, Description, StartTime, EndTime, Location, CreatorId, DayId, ParentId)
                            VALUES (@Id, @Title, @Description, @StartTime, @EndTime, @Location, @CreatorId, @DayId, @ParentId)";

                connection.Open();
                connection.Execute(sql, newActivity);
            }
        }

        public void InsertTestMainActivity(MainActivity newActivity)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Activity] 
                            (Id, Title, Description, StartTime, EndTime, Location, CreatorId, DayId, IsFreeActivity)
                            VALUES (@Id, @Title, @Description, @StartTime, @EndTime, @Location, @CreatorId, @DayId, @IsFreeActivity)";

                connection.Open();
                connection.Execute(sql, newActivity);
            }
        }
    }
}