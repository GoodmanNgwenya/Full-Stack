using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using System.Collections.Generic;

namespace WebApi.Services
{
  public interface IAdvertService
  {
    //advert details
    List<AdvertModel> GetAllAdvert();
    AdvertModel GetAdvertById(int id);
    List<AdvertModel> GetAdvertsByUserId(int userId);
    List<AdvertModel> GetAdvertByProvince(string province);
    AdvertModel PostAdvert(AdvertModel advert);
    AdvertModel UpdateAdvert(AdvertModel advert);
    void Delete(int id);
    List<ProvinceModel> GetAllProvince();
    List<CityModel> GetCities(int id);
    bool IsValid(object value);
  }
}
