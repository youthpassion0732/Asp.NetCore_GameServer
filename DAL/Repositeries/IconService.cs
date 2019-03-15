using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntities;

namespace DAL
{
    public class IconService : IIconService
    {
        private readonly ApiDbContext dBContext;
        private IGenericService<Icon> iconRepo;

        public IconService(ApiDbContext dbContext, IGenericService<Icon> iconRepo)
        {
            this.dBContext = dbContext;
            this.iconRepo = iconRepo;
        }

        public int Add(Icon icon)
        {
            try
            {
                iconRepo.Add(icon);
                iconRepo.Save();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return icon.Id;
        }

        public List<Icon> Get()
        {
            return iconRepo.List().ToList();
        }

        public Icon Get(Expression<Func<Icon, bool>> predicate)
        {
            return iconRepo.Get(predicate);
        }

        public IQueryable<Icon> List(Expression<Func<Icon, bool>> predicate)
        {
            return iconRepo.List(predicate);
        }

        public void Update(Icon icon)
        {
            iconRepo.Update(icon);
            iconRepo.Save();
        }

        public void Delete(Icon icon)
        {
            iconRepo.Delete(icon);
            iconRepo.Save();
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
