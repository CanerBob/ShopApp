using Microsoft.EntityFrameworkCore;
using ShopAPP.Models.Layer.Models;

namespace ShopAPP.Models.Layer.Database;
public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product {Id=1,Name = "Samsung S6",Url="telefon-samsungs5" ,Price = 2000, Description = "İyi Telefon", ImageUrl = "1.jpg", IsApproved = true},
            new Product { Id = 2, Name = "Samsung S7", Url = "telefon-samsungs7", Price = 3000, Description = "Güzel Telefon", ImageUrl = "2.jpg", IsApproved = true },
            new Product { Id = 3, Name = "Samsung S8", Url = "telefon-samsungs8", Price = 3000, Description = "Süper Telefon", ImageUrl = "3.jpg", IsApproved = false },
            new Product { Id = 4, Name = "Samsung S9", Url = "telefon-samsungs9", Price = 4000, Description = "İdare Eder ", ImageUrl = "4.jpg", IsApproved = false },
            new Product { Id = 5, Name = "Iphone 6S", Url = "telefon-iphone6s", Price = 5000, Description = "İyi Telefon", ImageUrl = "5.jpg", IsApproved = true},
            new Product { Id = 6, Name = "Iphone 7S", Url = "telefon-iphone7s", Price = 6000, Description = "İyi Telefon", ImageUrl = "6.jpg", IsApproved = false},
            new Product { Id = 7, Name = "Iphone 8S", Url = "telefon-iphone8s", Price = 7000, Description = "İyi Telefon", ImageUrl = "7.jpg", IsApproved = true});
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Telefon",Url="telefon" },
            new Category { Id = 2, Name = "Bilgisayar",Url="bilgisayar" },
            new Category { Id = 3, Name = "Elektronik Aletler",Url="elektronik" },
            new Category { Id = 4, Name = "Beyaz Esya",Url="beyaz-esya" });
        modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory {ProductId=1,CategoryId=1},
            new ProductCategory {ProductId=2,CategoryId=1},
            new ProductCategory {ProductId=3,CategoryId=3},
            new ProductCategory {ProductId=4,CategoryId=4},
            new ProductCategory {ProductId=5,CategoryId=1},
            new ProductCategory {ProductId=6,CategoryId=1},
            new ProductCategory {ProductId=7,CategoryId=1}
            );
    }
}