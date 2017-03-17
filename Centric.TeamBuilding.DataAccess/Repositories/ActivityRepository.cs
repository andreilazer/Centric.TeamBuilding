using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Centric.TeamBuilding.Entities;
using Dapper;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public class ActivityRepository
    {
        public void CreateMainActivity(MainActivity activity)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Activity] 
                            (Title, Description, StartTime, EndTime, Location, CreatorId, DayId, IsFreeActivity)
                            VALUES (@Title, @Description, @StartTime, @EndTime, @Location, @CreatorId, @DayId, @IsFreeActivity)";

                connection.Open();
                connection.Execute(sql, activity);
            }
        }

        public void CreateEmployeeActivity(EmployeeActivity activity)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[Activity] 
                            (Title, Description, StartTime, EndTime, Location, CreatorId, DayId, ParentId)
                            VALUES (@Title, @Description, @StartTime, @EndTime, @Location, @CreatorId, @DayId, @ParentId)";

                connection.Open();
                connection.Execute(sql, activity);
            }
        }

        public IEnumerable<MainActivity> GetDayActivities(Guid dayId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[Activity] WHERE DayId = @DayId";

                connection.Open();
                return connection.Query<MainActivity>(sql, new { DayId = dayId});
            }
        }

        public IEnumerable<EmployeeActivity> GetEmployeeActivities(Guid mainActivityId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[Activity] WHERE ParentId = @MainActivityId";

                connection.Open();
                return connection.Query<EmployeeActivity>(sql, new { MainActivityId = mainActivityId });
            }
        }

        public void AddUserToActivity(Guid userId, Guid activityId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[ActivityUsers] (ActivityId, UserId) VALUES (@ActivityId, @UserId)";

                connection.Open();
                connection.Execute(sql, new {ActivityId = activityId, UserId = userId });
            }
        }

        public IEnumerable<User> GetActivityParticipants(Guid activityId)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"SELECT u.* FROM [dbo].[ActivityUsers] au
                          JOIN [dbo].[User] u on au.UserId = u.Id
                          WHERE au.ActivityId = @ActivityId";

                connection.Open();
                return connection.Query<User>(sql, new { ActivityId = activityId });
            }
        }
    }
}