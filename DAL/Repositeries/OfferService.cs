using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class OfferService : IOfferService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<Offer> offerRepo;

        public OfferService(ApiDbContext dbContext, IGenericService<Offer> offerRepo)
        {
            this.dBContext = dbContext;
            this.offerRepo = offerRepo;
        }

        public int Add(Offer offer)
        {
            try
            {
                offerRepo.Add(offer);
                offerRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return offer.Id;
        }

        public List<Offer> Get()
        {
            return offerRepo.List().ToList();
        }

        public Offer Get(Expression<Func<Offer, bool>> predicate)
        {
            return offerRepo.Get(predicate);
        }

        public void Update(Offer offer)
        {
            offerRepo.Update(offer);
            offerRepo.Save();
        }

        public void Delete(Offer offer)
        {
            offerRepo.Delete(offer);
            offerRepo.Save();
        }

        public void BeginTransaction()
        {
            try
            {
                this.dBContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                this.dBContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                this.dBContext.Database.RollbackTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
