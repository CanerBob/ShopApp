using Microsoft.EntityFrameworkCore;
using ShopAPP.Models.Layer.Models;
using System.Reflection;

namespace ShopAPP.Models.Layer.Database;
public class APPDbContext:DbContext
{
    //public APPDbContext(DbContextOptions<APPDbContext> options):base(options){}
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=CANER\\SQLEXPRESS;Integrated Security=True;Database=ShopAPPDb;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
        modelBuilder.Entity<ProductCategory>()
            .HasKey(x => new { x.CategoryId, x.ProductId });
        //Bütün assemblyleri tarar ver sisteme dahil eder!!!!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}