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
    List<Province> GetProvince();
    List<City> GetCities();
    City GetCity(int id);

    //seller
    List<Seller> GetSellers();
    Seller GetSeller(int id);
    Seller UpdateSeller(Seller seller);

  }
}
