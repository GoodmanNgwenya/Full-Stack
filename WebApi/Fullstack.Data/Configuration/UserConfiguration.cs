using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("Users");
      builder.Property(u => u.Id)
        .HasDefaultValue(true);
      builder.HasData(
       new User
       {
         Id=100,
         FirstName="Test",
         LastName="Testing",
         Username="test@gmail.com",
         Role="User"
       });
    }
  }
}
