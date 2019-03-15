using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IIconService
    {
        int Add(Icon icon);
        List<Icon> Get();
        Icon Get(Expression<Func<Icon, bool>> predicate);
        IQueryable<Icon> List(Expression<Func<Icon, bool>> predicate);
        void Update(Icon icon);
        void Delete(Icon icon);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
