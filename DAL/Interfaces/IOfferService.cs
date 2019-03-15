using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL
{
    public interface IOfferService
    {
        int Add(Offer offer);
        List<Offer> Get();
        Offer Get(Expression<Func<Offer, bool>> predicate);
        void Update(Offer offer);
        void Delete(Offer offer);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
