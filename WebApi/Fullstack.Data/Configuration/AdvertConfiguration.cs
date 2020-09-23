using Fullstack.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fullstack.Data.Configuration
{
  public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
  {
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
      builder.ToTable("Adverts");

      builder.HasData(
        new Advert
        {
          Id=1,
          AdvertHeadlineText="Waterfall Estate: Property and Houses for sale",
          AdvertDetails="5 bedrooms, 2 garages and two bathrooms",
          Province="Mpumalanga",
          City= "Nelspruit",
          Price=10500000,
          AdvertStatus="live",
          UserId=1
        }
        //new Advert
        //{
        //  Id = 2,
        //  AdvertHeadlineText = "Halfway House Estate: Property and Houses for sale",
        //  AdvertDetails = "6 bedrooms, 3 garages and two bathrooms",
        //  Province = "Gauteng",
        //  City = "Pretoria",
        //  Price = 50500000,
        //  AdvertStatus = "live",
        //  UserId = 2,
        //},
        //new Advert
        //{
        //  Id = 3,
        //  AdvertHeadlineText = "Golf Estate: Property and Houses for sale",
        //  AdvertDetails = "6 bedrooms, 4 garages and two bathrooms",
        //  Province = "Western Cape",
        //  City = "Cape Town",
        //  Price = 60500000,
        //  AdvertStatus = "live",
        //  UserId = 2
        //}
        );

    }
  }
}
