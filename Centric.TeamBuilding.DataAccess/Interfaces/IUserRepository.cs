using Centric.TeamBuilding.Entities;
using System;

namespace Centric.TeamBuilding.DataAccess.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string email);
        User GetUser(Guid email);
        void InsertUser(User user);
    }
}