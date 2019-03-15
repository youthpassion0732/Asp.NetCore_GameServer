using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class QuizOfferService : IQuizOfferService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<QuizOffer> offerRepo;

        public QuizOfferService(ApiDbContext dbContext, IGenericService<QuizOffer> offerRepo)
        {
            this.dBContext = dbContext;
            this.offerRepo = offerRepo;
        }

        public int Add(QuizOffer quizOffer)
        {
            try
            {
                offerRepo.Add(quizOffer);
                offerRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return quizOffer.Id;
        }

        public List<QuizOffer> Get()
        {
            return offerRepo.List().ToList();
        }

        public QuizOffer Get(Expression<Func<QuizOffer, bool>> predicate)
        {
            return offerRepo.Get(predicate);
        }

        public void Update(QuizOffer quizOffer)
        {
            offerRepo.Update(quizOffer);
            offerRepo.Save();
        }

        public void Delete(QuizOffer quizOffer)
        {
            offerRepo.Delete(quizOffer);
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
