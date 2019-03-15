using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class GameService : IGameService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<Game> gameRepo;

        public GameService(ApiDbContext dbContext, IGenericService<Game> gameRepo)
        {
            this.dBContext = dbContext;
            this.gameRepo = gameRepo;
        }

        public int Add(Game game)
        {
            try
            {
                gameRepo.Add(game);
                gameRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return game.Id;
        }

        public List<Game> Get()
        {
            return gameRepo.List().ToList();
        }

        public Game Get(Expression<Func<Game, bool>> predicate)
        {
            return gameRepo.Get(predicate);
        }

        public void Update(Game game)
        {
            gameRepo.Update(game);
            gameRepo.Save();
        }

        public void Delete(Game game)
        {
            gameRepo.Delete(game);
            gameRepo.Save();
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
