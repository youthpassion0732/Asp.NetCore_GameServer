using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL
{
    public interface IQuizSummaryService
    {
        int Add(QuizSummary quizSummary);
        List<QuizSummary> Get();
        QuizSummary Get(Expression<Func<QuizSummary, bool>> predicate);
        IQueryable<QuizSummary> List(Expression<Func<QuizSummary, bool>> predicate);
        void Update(QuizSummary quizSummary);
        void Delete(QuizSummary quizSummary);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
