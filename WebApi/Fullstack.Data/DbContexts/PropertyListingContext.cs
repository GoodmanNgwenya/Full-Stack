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

  }
}
