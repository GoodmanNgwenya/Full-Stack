using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Configuration
{
  public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
  {
    public void Configure(EntityTypeBuilder<Province> builder)
    {
      builder.ToTable("Provinces");
      builder.HasData(
        new Province
        {
          Id=1,
          ProvinceName="Mpumalanga"
        },
        new Province
        {
          Id = 2,
          ProvinceName = "Gauteng"
        },
        new Province
        {
          Id = 3,
          ProvinceName = "Western Cape"
        },
        new Province
        {
          Id = 4,
          ProvinceName = "North West"
        },
        new Province
        {
          Id = 5,
          ProvinceName = "Free State"
        }
        );
    }
  }
}
