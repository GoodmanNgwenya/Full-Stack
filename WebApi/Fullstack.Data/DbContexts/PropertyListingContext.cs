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
      //Configure Column
      modelBuilder.Entity<Advert>()
                  .Property(p => p.Price)
                  .HasColumnType("decimal(18, 2)");
    }
  }
}
