using Microsoft.EntityFrameworkCore;
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

        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
          return await  _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithFiltersAsync(Expression<Func<T,bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
       
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
