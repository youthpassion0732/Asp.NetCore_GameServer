using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GenericService<T>: IGenericService<T> where T : class
    {
        private readonly ApiDbContext dBContext;

        public GenericService(ApiDbContext dbContext)
        {
            this.dBContext = dbContext;
        }

        public void Save()
        {
            try
            {
                this.dBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Add(T entity)
        {
            try
            {
                dBContext.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return dBContext.Set<T>().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> List(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                if (predicate != null)
                    return dBContext.Set<T>().Where(predicate);
                else
                    return dBContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSourceResult PagedList(int pageNo, int pageSize, Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                IQueryable<T> query = null;

                int skipRecords = pageNo <= 1 ? 0 : (pageNo - 1) * pageSize;

                int count = 0;

                query = dBContext.Set<T>();

                // search
                if (predicate != null)
                    query = query.Where(predicate);

                count = query.Count();

                query = query.Skip(skipRecords).Take(pageSize);

                DataSourceResult result = new DataSourceResult
                {
                    Data = query.ToList(),
                    Total = count
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSourceResult PagedList(int pageNo, int pageSize, List<Expression<Func<T, bool>>> predicates = null)
        {
            try
            {
                IQueryable<T> query = null;

                int skipRecords = pageNo <= 1 ? 0 : (pageNo - 1) * pageSize;

                int count = 0;

                query = dBContext.Set<T>();

                // search
                if (predicates != null && predicates.Count > 0)
                {
                    foreach (var predicate in predicates)
                        query = query.Where(predicate);
                }

                count = query.Count();

                query = query.Skip(skipRecords).Take(pageSize);

                DataSourceResult result = new DataSourceResult
                {
                    Data = query.ToList(),
                    Total = count
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                dBContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                dBContext.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                IQueryable<T> query = null;

                query = dBContext.Set<T>();

                // search
                if (predicate != null)
                    query = query.Where(predicate);

                return query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
