using Fullstack.Data.DbContexts;
using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
  public class AdvertService:IAdvertService
  {
    private IFullStackRepository _repo;
    public AdvertService(IFullStackRepository repo)
    {
      this._repo = repo;
    }
    //Advert crud operation
    public List<AdvertModel> GetAllAdvert()
    {
      var advertList = _repo.GetAdverts().Where(x => x.AdvertStatus == "live");
      return advertList.Select(u => MapAdvert(u)).ToList();
    }

    public AdvertModel GetAdvertById(int id)
    {
      var advertEntity = _repo.GetAdvert(id);
      if (advertEntity == null) return null;

      return MapAdvert(advertEntity);
    }

    public List<AdvertModel> GetAdvertsByUserId(int userId)
    {
      var advertList = _repo.GetAdverts();
      return advertList.Select(u => MapAdvert(u)).Where(s => s.UserId == userId && (s.AdvertStatus == "live" || s.AdvertStatus == "hiden")).ToList();
    }

    public List<AdvertModel> GetAdvertByProvince(string province)
    {
      var advertList = _repo.GetAdverts();
      return advertList.Select(u => MapAdvert(u)).Where(s => s.Province == province && (s.AdvertStatus == "live" || s.AdvertStatus == "hiden")).OrderByDescending(p=>p.Price).ToList();
    }

    public AdvertModel PostAdvert(AdvertModel model)
    {
      var advert = MapCreateAdvert(model);
      _repo.AddAdvert(advert);
      return model;

    }

    //public Advert UpdateAdvert(Advert advertParam)
    //{
    //  _repo.UpdateAdvert(advertParam);
    //  return advertParam;
    //}
    public AdvertModel UpdateAdvert(AdvertModel model)
    {
      var advert = MapCreateAdvert(model);
      _repo.UpdateAdvert(advert);
      return model;
    }

    public void Delete(int id)
    {
      _repo.DeleteAdvert(id);
    }
    public virtual bool IsValid(object value)
    {
      return !string.IsNullOrWhiteSpace(value.ToString());
    }

    //return overriden ValidationResult
    public virtual ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (IsValid(value)) return ValidationResult.Success;
      var message = "Ops";
      return new ValidationResult(message);
    }
    private AdvertModel MapAdvert(Advert advert)
    {
      return new AdvertModel
      {
        Id = advert.Id,
        AdvertHeadlineText = advert.AdvertHeadlineText,
        Province = advert.Province,
        City = advert.City,
        Price = advert.Price,
        AdvertDetails = advert.AdvertDetails,
        UserId = advert.UserId,
        ReleaseDate = advert.ReleaseDate,
        AdvertStatus = advert.AdvertStatus

      };
    }
    private Advert MapCreateAdvert(AdvertModel model)
    {
      return new Advert
      {
        Id=model.Id,
        AdvertHeadlineText = model.AdvertHeadlineText,
        AdvertDetails = model.AdvertDetails,
        Province = model.Province,
        City = model.City,
        Price = model.Price,
        UserId = model.UserId,
        ReleaseDate = model.ReleaseDate,
        AdvertStatus = model.AdvertStatus
      };
    }

    //province and city dropdown
    public List<ProvinceModel> GetAllProvince()
    {
      var provinceList = _repo.GetProvince();
      return provinceList.Select(u => MapProvince(u)).ToList();
    }

    public List<CityModel> GetCities(int provinceId)
    {
      var cityList = _repo.GetCities();
      return cityList.Select(u => MapCity(u)).Where(s => s.ProvinceId == provinceId).ToList();
    }

    private CityModel MapCity(City entityCity)
    {
      return new CityModel
      {
        Id = entityCity.Id,
        City = entityCity.CityName,
        ProvinceId = entityCity.ProvinceId
      };
    }

    private ProvinceModel MapProvince(Province entityProvince)
    {
      return new ProvinceModel
      {
        Id = entityProvince.Id,
        Province = entityProvince.ProvinceName
      };
    }

  }
}
