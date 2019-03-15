using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IQuestionService
    {
        int Add(Question question);
        List<Question> Get();
        Question Get(Expression<Func<Question, bool>> predicate);
        IQueryable<Question> List(Expression<Func<Question, bool>> predicate);
        void Update(Question question);
        void Delete(Question question);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
