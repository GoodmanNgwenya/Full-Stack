using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using System.Collections.Generic;

namespace WebApi.Services
{
  public interface IUserService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
   // IEnumerable<User> GetAll();
    IEnumerable<UserModel> GetAll();
    //User GetById(int id);
    UserModel GetById(int id);
    User CreateUser(User user,string password);

    //advert details
    IEnumerable<AdvertModel> GetAllAdvert();
    AdvertModel GetAdvertById(int id);
    IEnumerable<AdvertModel> GetAdvertsById(int id);
    Advert PostAdvert(Advert advert);
    Advert UpdateAdvert(Advert advert);
  }
}
