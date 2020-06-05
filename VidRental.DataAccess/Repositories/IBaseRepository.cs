using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VidRental.DataAccess.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void AddRange(IEnumerable<T> items);
        Task CreateAsync(T newItem);
        void Delete(T item);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
        void Update(T item);
    }
}