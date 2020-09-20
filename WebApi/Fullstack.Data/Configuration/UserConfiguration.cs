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

      builder.HasData(
       new User
       {
         Id=1,
         FirstName="Test",
         LastName="Testing",
         Username="test@gmail.com",
         Role="User"
       });
    }
  }
}
