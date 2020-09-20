﻿// <auto-generated />
using System;
using Fullstack.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fullstack.Data.Migrations
{
    [DbContext(typeof(PropertyListingContext))]
    partial class PropertyListingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fullstack.Data.Entities.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdvertDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvertHeadlineText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdvertStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageUrl")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("Fullstack.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "Nelspruit",
                            ProvinceId = 1
                        },
                        new
                        {
                            Id = 2,
                            CityName = "Witbank",
                            ProvinceId = 1
                        },
                        new
                        {
                            Id = 3,
                            CityName = "Pretoria",
                            ProvinceId = 2
                        },
                        new
                        {
                            Id = 4,
                            CityName = "Cape Town",
                            ProvinceId = 3
                        },
                        new
                        {
                            Id = 5,
                            CityName = "Stellenbosch",
                            ProvinceId = 3
                        },
                        new
                        {
                            Id = 6,
                            CityName = "Johannesburg",
                            ProvinceId = 2
                        },
                        new
                        {
                            Id = 7,
                            CityName = "Bloemfontein",
                            ProvinceId = 5
                        },
                        new
                        {
                            Id = 8,
                            CityName = "Marikana",
                            ProvinceId = 4
                        },
                        new
                        {
                            Id = 9,
                            CityName = "Welkom",
                            ProvinceId = 5
                        });
                });

            modelBuilder.Entity("Fullstack.Data.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProvinceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Provinces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProvinceName = "Mpumalanga"
                        },
                        new
                        {
                            Id = 2,
                            ProvinceName = "Gauteng"
                        },
                        new
                        {
                            Id = 3,
                            ProvinceName = "Western Cape"
                        },
                        new
                        {
                            Id = 4,
                            ProvinceName = "North West"
                        },
                        new
                        {
                            Id = 5,
                            ProvinceName = "Free State"
                        });
                });

            modelBuilder.Entity("Fullstack.Data.Entities.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Fullstack.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fullstack.Data.Entities.Advert", b =>
                {
                    b.HasOne("Fullstack.Data.Entities.User", "User")
                        .WithMany("adverts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fullstack.Data.Entities.City", b =>
                {
                    b.HasOne("Fullstack.Data.Entities.Province", null)
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fullstack.Data.Entities.Seller", b =>
                {
                    b.HasOne("Fullstack.Data.Entities.User", "User")
                        .WithMany("seller")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
