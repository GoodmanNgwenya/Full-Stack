using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using System.Collections.Generic;

namespace WebApi.Services
{
  public interface IUserService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    List<UserModel> GetAll();
    UserModel GetById(int id);
    User CreateUser(User user,string password);
    void Update(User userParam, string Password);

  }
}
