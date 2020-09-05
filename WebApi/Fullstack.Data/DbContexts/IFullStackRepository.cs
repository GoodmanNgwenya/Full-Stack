using Fullstack.Data.Entities;
using System.Collections.Generic;

namespace Fullstack.Data.DbContexts
{
  public interface IFullStackRepository
  {
    User GetUser(int id);
    List<User> GetUsers();
    User CreateUser(User user);
    User UpdateUser(User user);
    void DeleteUser(int id);
  }
}
