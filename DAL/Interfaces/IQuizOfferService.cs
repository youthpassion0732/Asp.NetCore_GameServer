using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    public interface IQuizOfferService
    {
        int Add(QuizOffer quizOffer);
        List<QuizOffer> Get();
        QuizOffer Get(Expression<Func<QuizOffer, bool>> predicate);
        void Update(QuizOffer quizOffer);
        void Delete(QuizOffer quizOffer);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
