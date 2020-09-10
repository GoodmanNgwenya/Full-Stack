using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Data.DbContexts
{
  public class PropertyListingContext : DbContext
  {
    public PropertyListingContext(DbContextOptions<PropertyListingContext> options)
      : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Province> Provinces { get; set; }
  }
}
