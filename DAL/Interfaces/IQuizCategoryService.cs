using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    public interface IQuizCategoryService
    {
        int Add(QuizCategory quizCategory);
        List<QuizCategory> Get();
        List<QuizCategory> List(Expression<Func<QuizCategory, bool>> predicate);
        QuizCategory Get(Expression<Func<QuizCategory, bool>> predicate);
        void Update(QuizCategory quizCategory);
        void Delete(QuizCategory quizCategory);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
