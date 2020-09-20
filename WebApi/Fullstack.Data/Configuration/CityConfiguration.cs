using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Configuration
{
  public class CityConfiguration : IEntityTypeConfiguration<City>
  {
    public void Configure(EntityTypeBuilder<City> builder)
    {
      builder.ToTable("Cities");
      builder.HasData(
        new City
        {
          Id=1,
          CityName= "Nelspruit",
          ProvinceId=1
        },
        new City
        {
          Id = 2,
          CityName = "Witbank",
          ProvinceId = 1
        },
        new City
        {
          Id = 3,
          CityName = "Pretoria",
          ProvinceId = 2
        },
        new City
        {
          Id = 4,
          CityName = "Cape Town",
          ProvinceId = 3
        },
        new City
        {
          Id = 5,
          CityName = "Stellenbosch",
          ProvinceId = 3
        },
        new City
        {
          Id = 6,
          CityName = "Johannesburg",
          ProvinceId = 2
        }
        ,
        new City
        {
          Id = 7,
          CityName = "Bloemfontein",
          ProvinceId = 5
        },
        new City
        {
          Id = 8,
          CityName = "Marikana",
          ProvinceId = 4
        },
        new City
        {
          Id = 9,
          CityName = "Welkom",
          ProvinceId = 5
        }
        );
    }
  }
}
