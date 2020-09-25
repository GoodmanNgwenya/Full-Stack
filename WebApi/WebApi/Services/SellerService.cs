using Fullstack.Data.DbContexts;
using Fullstack.Data.Entities;
using Fullstack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
  public class SellerService : ISellerService
  {
    private IFullStackRepository _repo;
    public SellerService(IFullStackRepository repo)
    {
      this._repo = repo;
    }

    public List<SellerModel> GetAllSellers()
    {
      var sellerList = _repo.GetSellers();
      return sellerList.Select(u => MapGetSeller(u)).ToList();
    }
    public SellerModel GetSellerByUserId(int userId)
    {
      var sellerEntity = _repo.GetSeller(userId);

      if (sellerEntity == null)
      {
        var userEntity = _repo.GetUser(userId);
        return MapUserSeller(userEntity);
      }
      else
      {
        return MapGetSeller(sellerEntity);
      }
    }

    public SellerModel GetSellerPerAdvert(int advertId)
    {
      var advertEntity= _repo.GetAdverts().FirstOrDefault(u => u.Id == advertId);
      var sellerEntity = _repo.GetSeller(advertEntity.UserId);
      if (sellerEntity != null)
      {
        return MapGetSeller(sellerEntity);
      }
      return null;
    }
    public SellerModel UpdateSeller(SellerModel model)
    {
      var seller = MapSeller(model);
      _repo.UpdateSeller(seller);
      return model;
    }


    private SellerModel MapGetSeller(Seller model)
    {
      return new SellerModel
      {
        Id = model.Id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber,
        UserId = model.UserId
      };
    }
    //private User MapUserSeller(SellerModel model)
    //{
    //  return new User
    //  {
    //    FirstName = model.FirstName,
    //    LastName = model.LastName,
    //    Username = model.Email,
    //  };
    //}
    private SellerModel MapUserSeller(User model)
    {
      return new SellerModel
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Username
      };
    }
    private Seller MapSeller(SellerModel model)
    {
      return new Seller
      {
        Id = model.Id,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        PhoneNumber = model.PhoneNumber,
        UserId = model.UserId
      };
    }
  }
}
