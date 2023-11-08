using ShopAPP.Models.Layer.Models;
using ShopAPP.Repository.Layer.CategoryRepository;

namespace ShopAPP.Service.Layer.Services.CategoryService;
public class CategoryService : ICategoryService
{
    private readonly ICateogoryRepository _categoryRepository;

    public CategoryService(ICateogoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void Create(Category entity)
    {
        _categoryRepository.Create(entity);
    }

    public void Delete(Category entity)
    {
        _categoryRepository.Delete(entity);
    }

    public void DeleteFromCategory(int productId, int categoryId)
    {
        _categoryRepository.DeleteFromCategory(productId,categoryId);
    }

    public List<Category> GetAll()
    {
       return _categoryRepository.GetAll();
    }

    public Category GetById(int id)
    {
        return _categoryRepository.GetById(id);
    }

    public Category GetByIdWithProducts(int categoryId)
    {
       return _categoryRepository.GetByIdWithProducts(categoryId);
    }

    public void Update(Category entity)
    {
        _categoryRepository.Update(entity);
    }
}
