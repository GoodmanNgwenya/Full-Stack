using Fullstack.Data.Configuration;
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
    public DbSet<Seller> Sellers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
      modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
      modelBuilder.ApplyConfiguration(new CityConfiguration());
      //modelBuilder.ApplyConfiguration(new UserConfiguration());
      //modelBuilder.ApplyConfiguration(new AdvertConfiguration());

    }
  }
}
