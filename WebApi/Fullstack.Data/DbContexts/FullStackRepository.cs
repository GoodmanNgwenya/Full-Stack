using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Fullstack.Data.DbContexts
{
  public class FullStackRepository:IFullStackRepository
  {
    private  PropertyListingContext _context;

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
      _context.Users.Remove(entity); //CAREFULL!! here when you copy and paste, change _ctx.Users to the new DBSet
      _context.SaveChanges();
    }

  }
}
