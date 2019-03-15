using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public interface IGameUserService
    {
        int Add(GameUser obj);
        List<GameUser> Get();
        GameUser Get(Expression<Func<GameUser, bool>> predicate);
        void Update(GameUser obj);
        void Delete(GameUser obj);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
