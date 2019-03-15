using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class QuizCategoryService : IQuizCategoryService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<QuizCategory> offerRepo;

        public QuizCategoryService(ApiDbContext dbContext, IGenericService<QuizCategory> offerRepo)
        {
            this.dBContext = dbContext;
            this.offerRepo = offerRepo;
        }

        public int Add(QuizCategory quizCategory)
        {
            try
            {
                offerRepo.Add(quizCategory);
                offerRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return quizCategory.Id;
        }

        public List<QuizCategory> Get()
        {
            return offerRepo.List().ToList();
        }

        public List<QuizCategory> List(Expression<Func<QuizCategory, bool>> predicate)
        {
            return offerRepo.List(predicate).ToList();
        }

        public QuizCategory Get(Expression<Func<QuizCategory, bool>> predicate)
        {
            return offerRepo.Get(predicate);
        }

        public void Update(QuizCategory quizCategory)
        {
            offerRepo.Update(quizCategory);
            offerRepo.Save();
        }

        public void Delete(QuizCategory quizCategory)
        {
            offerRepo.Delete(quizCategory);
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
