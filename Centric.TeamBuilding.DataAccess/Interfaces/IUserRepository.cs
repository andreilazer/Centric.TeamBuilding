using Centric.TeamBuilding.Entities;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string email);
        void InsertUser(User user);
    }
}