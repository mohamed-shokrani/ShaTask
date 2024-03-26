using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShaTask.Data;
using ShaTask.Interfaces;
using System.Linq.Expressions;

namespace ShaTask.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
           await _context.AddAsync(entity);
             
        }

        public async Task DeleteAsync(Expression<Func<T,bool>> expression)
        {
         await   _context.Set<T>().Where(expression).ExecuteDeleteAsync();
        }
        public void  Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public async Task<int> Update(Expression<Func<T, bool>> criteria, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> prop)
        {
            _context.Set<T>().Where(criteria).ExecuteUpdate(prop);
            return 1;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
          
           
        }
        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByIdAsyncWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                   query = query.Include(item);
                }
            }
            return await query.Where(expression).FirstOrDefaultAsync(expression);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T,bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> CheckEntityExistsAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _context.Set<T>().AnyAsync(expression);
        }




        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression =null,params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.AsQueryable();
        }

        public async Task  UpdateAsync(T entity)
        {
             _context.Update(entity);
        }
    }
}
