using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    public class GameUserService : IGameUserService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<GameUser> gameUserRepo;

        public GameUserService(ApiDbContext dbContext, IGenericService<GameUser> gameUserRepo)
        {
            this.dBContext = dbContext;
            this.gameUserRepo = gameUserRepo;
        }

        public int Add(GameUser obj)
        {
            try
            {
                gameUserRepo.Add(obj);
                gameUserRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return obj.Id;
        }

        public List<GameUser> Get()
        {
            return gameUserRepo.List().ToList();
        }

        public GameUser Get(Expression<Func<GameUser, bool>> predicate)
        {
            return gameUserRepo.Get(predicate);
        }

        public void Update(GameUser obj)
        {
            gameUserRepo.Update(obj);
            gameUserRepo.Save();
        }

        public void Delete(GameUser obj)
        {
            gameUserRepo.Delete(obj);
            gameUserRepo.Save();
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
