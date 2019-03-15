using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    public interface IGameService
    {
        int Add(Game game);
        List<Game> Get();
        Game Get(Expression<Func<Game, bool>> predicate);
        void Update(Game game);
        void Delete(Game game);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
