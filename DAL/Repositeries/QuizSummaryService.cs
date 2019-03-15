
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class QuizSummaryService : IQuizSummaryService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<QuizSummary> quizSummaryRepo;

        public QuizSummaryService(ApiDbContext dbContext, IGenericService<QuizSummary> quizSummaryRepo)
        {
            this.dBContext = dbContext;
            this.quizSummaryRepo = quizSummaryRepo;
        }

        public int Add(QuizSummary quizSummary)
        {
            try
            {
                quizSummaryRepo.Add(quizSummary);
                quizSummaryRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return quizSummary.Id;
        }

        public List<QuizSummary> Get()
        {
            return quizSummaryRepo.List().ToList();
        }

        public QuizSummary Get(Expression<Func<QuizSummary, bool>> predicate)
        {
            return quizSummaryRepo.Get(predicate);
        }

        public IQueryable<QuizSummary> List(Expression<Func<QuizSummary, bool>> predicate)
        {
            return quizSummaryRepo.List(predicate);
        }

        public void Update(QuizSummary quizSummary)
        {
            quizSummaryRepo.Update(quizSummary);
            quizSummaryRepo.Save();
        }

        public void Delete(QuizSummary quizSummary)
        {
            quizSummaryRepo.Delete(quizSummary);
            quizSummaryRepo.Save();
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
