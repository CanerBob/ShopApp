﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopAPP.Models.Layer.Database;

#nullable disable

namespace ShopAPP.Models.Layer.Migrations
{
    [DbContext(typeof(APPDbContext))]
    [Migration("20231014104232_Mig_1")]
    partial class Mig_1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Telefon",
                            Url = "telefon"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bilgisayar",
                            Url = "bilgisayar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Elektronik Aletler",
                            Url = "elektronik"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Beyaz Esya",
                            Url = "beyaz-esya"
                        });
                });

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHome")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "İyi Telefon",
                            ImageUrl = "1.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Samsung S6",
                            Price = 2000.0,
                            Url = "telefon-samsungs5"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Güzel Telefon",
                            ImageUrl = "2.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Samsung S7",
                            Price = 3000.0,
                            Url = "telefon-samsungs7"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Süper Telefon",
                            ImageUrl = "3.jpg",
                            IsApproved = false,
                            IsHome = false,
                            Name = "Samsung S8",
                            Price = 3000.0,
                            Url = "telefon-samsungs8"
                        },
                        new
                        {
                            Id = 4,
                            Description = "İdare Eder ",
                            ImageUrl = "4.jpg",
                            IsApproved = false,
                            IsHome = false,
                            Name = "Samsung S9",
                            Price = 4000.0,
                            Url = "telefon-samsungs9"
                        },
                        new
                        {
                            Id = 5,
                            Description = "İyi Telefon",
                            ImageUrl = "5.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Iphone 6S",
                            Price = 5000.0,
                            Url = "telefon-iphone6s"
                        },
                        new
                        {
                            Id = 6,
                            Description = "İyi Telefon",
                            ImageUrl = "6.jpg",
                            IsApproved = false,
                            IsHome = false,
                            Name = "Iphone 7S",
                            Price = 6000.0,
                            Url = "telefon-iphone7s"
                        },
                        new
                        {
                            Id = 7,
                            Description = "İyi Telefon",
                            ImageUrl = "7.jpg",
                            IsApproved = true,
                            IsHome = false,
                            Name = "Iphone 8S",
                            Price = 7000.0,
                            Url = "telefon-iphone8s"
                        });
                });

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 4,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 5
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 6
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 7
                        });
                });

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.ProductCategory", b =>
                {
                    b.HasOne("ShopAPP.Models.Layer.Models.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopAPP.Models.Layer.Models.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("ShopAPP.Models.Layer.Models.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
