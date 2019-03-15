using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IAnswerService
    {
        int Add(Answer answer);
        List<Answer> Get();
        Answer Get(Expression<Func<Answer, bool>> predicate);
        IQueryable<Answer> List(Expression<Func<Answer, bool>> predicate);
        void Update(Answer answer);
        void Delete(Answer answer);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
