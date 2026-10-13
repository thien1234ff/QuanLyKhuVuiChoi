using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QuanLyKhuVuiChoi.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T? GetById(object id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        int SaveChanges();
    }
}
