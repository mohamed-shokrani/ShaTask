using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ShaTask.Interfaces
{
    public interface IGenericRepository<T> where T :class 
    {
        Task<int> Update(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> prop);        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includes);
         Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
    }
}
