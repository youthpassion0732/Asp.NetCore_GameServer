using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IGenericService<T> where T: class
    {
        void Save();

        void Add(T entity);

        T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> List(Expression<Func<T, bool>> predicate = null);

        DataSourceResult PagedList(int pageNo, int pageSize, Expression<Func<T, bool>> predicate = null);

        DataSourceResult PagedList(int pageNo, int pageSize, List<Expression<Func<T, bool>>> predicates = null);

        void Update(T entity);

        void Delete(T entity);

        int Count(Expression<Func<T, bool>> predicate = null);

    }
}
