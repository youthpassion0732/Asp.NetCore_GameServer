using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class QuizHistoryService : IQuizHistoryService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<QuizHistory> quizHistoryRepo;

        public QuizHistoryService(ApiDbContext dbContext, IGenericService<QuizHistory> quizHistoryRepo)
        {
            this.dBContext = dbContext;
            this.quizHistoryRepo = quizHistoryRepo;
        }

        public int Add(QuizHistory quizHistory)
        {
            try
            {
                quizHistoryRepo.Add(quizHistory);
                quizHistoryRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return quizHistory.Id;
        }

        public List<QuizHistory> Get()
        {
            return quizHistoryRepo.List().ToList();
        }

        public QuizHistory Get(Expression<Func<QuizHistory, bool>> predicate)
        {
            return quizHistoryRepo.Get(predicate);
        }

        public IQueryable<QuizHistory> List(Expression<Func<QuizHistory, bool>> predicate)
        {
            return quizHistoryRepo.List(predicate);
        }

        public void Update(QuizHistory quizHistory)
        {
            quizHistoryRepo.Update(quizHistory);
            quizHistoryRepo.Save();
        }

        public void Delete(QuizHistory quizHistory)
        {
            quizHistoryRepo.Delete(quizHistory);
            quizHistoryRepo.Save();
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
