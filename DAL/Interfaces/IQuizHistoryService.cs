using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IQuizHistoryService
    {
        int Add(QuizHistory quizHistory);
        List<QuizHistory> Get();
        QuizHistory Get(Expression<Func<QuizHistory, bool>> predicate);
        IQueryable<QuizHistory> List(Expression<Func<QuizHistory, bool>> predicate);
        void Update(QuizHistory quizHistory);
        void Delete(QuizHistory quizHistory);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
