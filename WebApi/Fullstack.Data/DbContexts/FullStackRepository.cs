using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Fullstack.Data.DbContexts
{
  public class FullStackRepository : IFullStackRepository
  {
    private PropertyListingContext _context;

    public FullStackRepository(PropertyListingContext context)
    {
      _context = context;
    }


    public List<User> GetUsers()
    {
      return _context.Users.ToList();
    }

    public User GetUser(int id)
    {
      return _context.Users.Find(id);
    }

    public User CreateUser(User user)
    {

      _context.Users.Add(user);
      _context.SaveChanges();
      return user;
    }

    public User UpdateUser(User user)
    {
      var existing = _context.Users.SingleOrDefault(em => em.Id == user.Id);
      if (existing == null) return null;

      _context.Entry(existing).State = EntityState.Detached;
      _context.Users.Attach(user);
      _context.Entry(user).State = EntityState.Modified;
      _context.SaveChanges();
      return user;
    }

    public void DeleteUser(int id)
    {
      var entity = _context.Users.Find(id);
      _context.Users.Remove(entity);
      _context.SaveChanges();
    }

    //crud operation for advert
    public List<Advert> GetAdverts()
    {
      return _context.Adverts.ToList();
    }

    public Advert GetAdvert(int id)
    {
      return _context.Adverts.Find(id);
    }

    public Advert AddAdvert(Advert advert)
    {

      try
      {
        if (_context != null)
        {
          advert.AdvertStatus = "live";
          _context.Add(advert);
          _context.SaveChanges();
          return advert;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public Advert UpdateAdvert(Advert advert)
    {
      try
      {
        if (_context != null)
        {
          _context.Update(advert);
          _context.SaveChanges();
          return advert;
        }
        else
        {
          return null;
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public void DeleteAdvert(int id)
    {
      var entity = _context.Adverts.Find(id);
      entity.AdvertStatus = "deleted";
      _context.Adverts.Update(entity);
      _context.SaveChanges();
    }

    //populate province and city dropdown
    //crud operation for advert
    public List<Province> GetProvince()
    {
      return _context.Provinces.ToList();
    }
    public List<City> GetCities()
    {
      return _context.Cities.ToList();
    }
    public City GetCity(int id)
    {
      return _context.Cities.Find(id);
    }


    //seller crud operstion
    public List<Seller> GetSellers()
    {
      return _context.Sellers.ToList();

    }
    public Seller GetSeller(int id)
    {
      return _context.Sellers.FirstOrDefault(x => x.UserId == id);
    }

    public Seller UpdateSeller(Seller seller)
    {
      try
      {
        var existing = _context.Sellers.FirstOrDefault(em => em.UserId == seller.UserId);
        if (existing == null)
        {
          _context.Add(seller);
          _context.SaveChanges();
          return seller;
        }
        else
        {
          seller.Id = existing.Id;

          _context.Entry(existing).State = EntityState.Detached;
          _context.Sellers.Attach(seller);
          _context.Entry(seller).State = EntityState.Modified;
          _context.SaveChanges();
          return seller;

        }
        
      }
      catch (Exception ex)
      {
        return null;
      }
    }


  }
}
