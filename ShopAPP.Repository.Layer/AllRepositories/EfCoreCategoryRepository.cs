using Microsoft.EntityFrameworkCore;
using ShopAPP.Models.Layer.Database;
using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.CategoryRepository;
using ShopAPP.Repository.Layer.GenericRepositories;

namespace ShopAPP.Repository.Layer.AllRepositories;
public class EfCoreCategoryRepository : GenericRepository<Category, APPDbContext>, ICateogoryRepository
{
    public void DeleteFromCategory(int productId, int categoryId)
    {
        using (var context = new APPDbContext()) 
        {
            var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
            context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
        }
    }

    public Category GetByIdWithProducts(int categoryId)
    {
        using (var context = new APPDbContext()) 
        {
            return context.Categories
                    .Where(x => x.Id == categoryId)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
        }
    }
}
