using System.Linq.Expressions;

namespace ShaTask.Interfaces
{
    public interface IGenericRepository<T> where T :class 
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
    }
}
