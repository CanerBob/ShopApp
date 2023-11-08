using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.GenericRepositories;

namespace ShopAPP.Repository.Layer.CategoryRepository;
public interface ICateogoryRepository:IGenericRepository<Category>
{
    Category GetByIdWithProducts(int categoryId);
    void DeleteFromCategory(int productId, int categoryId);
}