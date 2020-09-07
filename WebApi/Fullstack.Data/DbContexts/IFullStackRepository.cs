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

    //advert
    Advert GetAdvert(int id);
    List<Advert> GetAdverts();
    Advert AddAdvert(Advert advert);
    Advert UpdateAdvert(Advert advert);
    void DeleteAdvert(int id);

    //dropdown
    List<EntityProvince> GetProvince();
    List<EntityCity> GetCities();
    EntityCity GetCity(int id);

  }
}
