using Microsoft.EntityFrameworkCore;

namespace ShopAPP.Repository.Layer.GenericRepositories;
public interface IGenericRepository<T> where T : class 
{
    T GetById(int id);
    List<T> GetAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
