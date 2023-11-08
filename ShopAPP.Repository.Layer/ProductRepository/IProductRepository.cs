using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.GenericRepositories;

namespace ShopAPP.Repository.Layer.ProductRepository;
public interface IProductRepository:IGenericRepository<Product>
{
    Product GetProductDetails(string url);
    void Update(Product entity, int[] categoryIds);
    Product GetByIdWithCategory(int id);
    List<Product> GetProductsByCategory(string name,int page,int pageSize);
    List<Product> GetSearchResult(string searchString);
    List<Product> GetHomePageProducts();
    int GetCountByCategory(string category);
}
