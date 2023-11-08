using Azure;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Models.Layer.Database;
using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.GenericRepositories;

namespace ShopAPP.Repository.Layer.ProductRepository;
public class ProductRepository : GenericRepository<Product,APPDbContext>,IProductRepository
{
    public Product GetByIdWithCategory(int id)
    {
        using (var context = new APPDbContext()) 
        {
            return context.Products
                          .Where(x => x.Id == id)
                          .Include(x => x.ProductCategories)
                          .ThenInclude(x => x.Category)
                          .FirstOrDefault();
        }
    }

    public int GetCountByCategory(string category)
    {
        using (var context = new APPDbContext())
        {
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(x => x.Category.Name == category));
            }
            return products.Count();
        }
    }

    public List<Product> GetHomePageProducts()
    {
        using (var context = new APPDbContext()) 
        {
            return context.Products.Where(x => x.IsApproved && x.IsHome == true).ToList();
        }
    }

    public List<Product> GetPopularProducts()
    {
        using (var context = new APPDbContext()) 
        {
        return context.Products.ToList();
        }
    }

    public Product GetProductDetails(string url)
    {
        using (var context = new APPDbContext())
        {
            return context.Products
                .Where(x => x.Url == url)
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefault();
        }
    }
    public List<Product> GetProductsByCategory(string name,int page,int pageSize)
    {
        using (var context = new APPDbContext()) 
        {
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(name)) 
            {
                products = products
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .Where(x => x.ProductCategories.Any(x => x.Category.Name.ToLower()==name.ToLower()));
            }
            return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
        }
    }

    public List<Product> GetSearchResult(string searchString)
    {
        using ( var context = new APPDbContext()) 
        {
            var products =
                    context
                    .Products
                    .Where(x => x.IsApproved && x.Name.Contains(searchString) || x.Description.Contains(searchString))
                    .AsQueryable();
           return products.ToList();
        }
    }

    public List<Product> GetTop5Products()
    {
        throw new NotImplementedException();
    }

    public void Update(Product entity, int[] categoryIds)
    {
        using (var context = new APPDbContext()) 
        {
            var product = context.Products
                .Include(x => x.ProductCategories)
                .FirstOrDefault(x => x.Id == entity.Id);
            if (product != null) 
            {
               product.Name = entity.Name;
                product.Description = entity.Description;
                product.Url = entity.Url;
                product.ImageUrl = entity.ImageUrl;
                product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                {
                    ProductId=entity.Id,
                    CategoryId= catid
                }).ToList();
                context.SaveChanges();
            }
        }
        
    }
}