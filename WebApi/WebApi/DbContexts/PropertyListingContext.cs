using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.DbContexts
{
  public class PropertyListingContext : DbContext
  {
    public PropertyListingContext(DbContextOptions<PropertyListingContext> options)
      :base(options)
    {
    }
    public DbSet<User> Users { get; set; }

  }
}
