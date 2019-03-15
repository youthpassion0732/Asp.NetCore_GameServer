using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class QuestionService : IQuestionService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<Question> questionRepo;

        public QuestionService(ApiDbContext dbContext, IGenericService<Question> questionRepo)
        {
            this.dBContext = dbContext;
            this.questionRepo = questionRepo;
        }

        public int Add(Question question)
        {
            try
            {
                questionRepo.Add(question);
                questionRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return question.Id;
        }

        public List<Question> Get()
        {
            return questionRepo.List().ToList();
        }

        public Question Get(Expression<Func<Question, bool>> predicate)
        {
            return questionRepo.Get(predicate);
        }

        public IQueryable<Question> List(Expression<Func<Question, bool>> predicate)
        {
            return questionRepo.List(predicate);
        }

        public void Update(Question question)
        {
            questionRepo.Update(question);
            questionRepo.Save();
        }

        public void Delete(Question question)
        {
            questionRepo.Delete(question);
            questionRepo.Save();
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
