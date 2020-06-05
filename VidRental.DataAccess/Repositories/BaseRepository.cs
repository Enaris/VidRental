using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VidRental.DataAccess.DataContext;

namespace VidRental.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected VidContext _context;

        public BaseRepository(VidContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            _context.Set<T>().AddRange(items);
        }

        public virtual async Task CreateAsync(T newItem)
        {
            await _context.Set<T>().AddAsync(newItem);
        }

        public virtual void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

        public virtual void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
