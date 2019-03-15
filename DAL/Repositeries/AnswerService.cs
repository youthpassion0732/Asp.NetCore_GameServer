using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class AnswerService : IAnswerService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<Answer> answerRepo;

        public AnswerService(ApiDbContext dbContext, IGenericService<Answer> answerRepo)
        {
            this.dBContext = dbContext;
            this.answerRepo = answerRepo;
        }

        public int Add(Answer answer)
        {
            try
            {
                answerRepo.Add(answer);
                answerRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return answer.Id;
        }

        public List<Answer> Get()
        {
            return answerRepo.List().ToList();
        }

        public Answer Get(Expression<Func<Answer, bool>> predicate)
        {
            return answerRepo.Get(predicate);
        }

        public IQueryable<Answer> List(Expression<Func<Answer, bool>> predicate)
        {
            return answerRepo.List(predicate);
        }

        public void Update(Answer answer)
        {
            answerRepo.Update(answer);
            answerRepo.Save();
        }

        public void Delete(Answer answer)
        {
            answerRepo.Delete(answer);
            answerRepo.Save();
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
