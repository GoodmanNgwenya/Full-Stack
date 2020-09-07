using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
      _context.Adverts.Remove(entity);
      _context.SaveChanges();
    }

    //populate province and city dropdown
    //crud operation for advert
    public List<EntityProvince> GetProvince()
    {
      return _context.Provinces.ToList();
    }
    public List<EntityCity> GetCities()
    {
      return _context.Cities.ToList();
    }
    public EntityCity GetCity(int id)
    {
      return _context.Cities.Find(id);
    }


  }
}
