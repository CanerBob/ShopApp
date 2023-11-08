using ShopAPP.Models.Layer.Models;

namespace ShopAPP.Service.Layer.Services.CategoryService;
public interface ICategoryService
{
    Category GetById(int id);
    Category GetByIdWithProducts(int categoryId);
    List<Category> GetAll();
    void Create(Category entity);
    void Update(Category entity);
    void Delete(Category entity);
    void DeleteFromCategory(int productId,int categoryId);
}
