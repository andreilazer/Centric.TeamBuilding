using System.Data.SqlClient;
using System.Linq;
using Centric.TeamBuilding.Entities;
using Dapper;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public class UserRepository
    {
        public User GetUser(string email)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"SELECT * FROM [dbo].[User] WHERE [Email] = @Email";

                connection.Open();
                return connection.Query<User>(sql, new {Email = email}).FirstOrDefault();
            }
        }

        public void InsertUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                var sql = @"INSERT INTO [dbo].[User]
                          (FirstName, LastName, Email, Password, Gender, PhoneNumber, BirthDate)
                          VALUES (@FirstName, @LastName, @Email, @Password, @Gender, @PhoneNumber, @BirthDate)";

                connection.Open();
                connection.Execute(sql, user);
            }
        }
    }
}