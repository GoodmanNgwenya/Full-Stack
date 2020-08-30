using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
  public interface IUserService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User CreateUser(User user,string password);
  }
}
