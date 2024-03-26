using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ShaTask.Interfaces
{
    public interface IGenericRepository<T> where T :class 
    {
        Task<int> Update(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> prop);  
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
         Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        Task<bool> CheckEntityExistsAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> GetByIdAsyncWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(long id);
        public void Delete(T entity);
    }
}
