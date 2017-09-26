using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketSaleCore.Models;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShFLY.DataBase.DAL.Specifications;
using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace ShFLY.DataBase.DAL.Implemtaations
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        private readonly DbContext context;
        private IDbSet<TEntity> entities;
        private DbSet<TEntity> DbSet;

        private IDbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<TEntity>();
                }
                return entities;
            }
        }

        private IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.DbSet = context.Set<TEntity>();

        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> query = Table;

            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);
            return query;
        }

        public void Create(TEntity entity, string createdBy = null)
        {
            DbSet.Add(entity);
        }
        public void Update(TEntity entity, string modifiedBy = null)
        {
            DbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(object id, string deleteBy = null)
        {
            TEntity entity = DbSet.Find(id);
            Delete(entity);
        }
        public void Delete(TEntity entity, string deleteBy = null)
        {

            if (context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        #region
        public virtual IEnumerable<TEntity> GetAll(
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? skip = null,
             int? take = null)
        {
            return GetQueryable(null, orderBy, skip, take).ToList();
        }
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null)
        {
            var ret = GetQueryable(filter, orderBy, skip, take).ToList();
            return ret;
        }
        public virtual TEntity GetById(object id)
        {
            var ret = DbSet.Find(id);
            return ret;
        }
        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var ret = GetQueryable(filter).Count();
            return ret;
        }
        public virtual bool GetExist(Expression<Func<TEntity, bool>> filter = null)
        {
            var ret = GetQueryable(filter).Any();
            return ret;
        }
        public virtual TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var ret = GetQueryable(filter, orderBy).FirstOrDefault();
            return ret;
        }
       
        #endregion
        
        public virtual TEntity GetOne(ISpecification<TEntity> specification = null)
        {
            var query = DbSet.Where(specification.IsSatisifiedBy());
            return query.SingleOrDefault();
        }
        public IEnumerable<TEntity> GetLocal()
        {
            return DbSet.Local;
        }
    }
}
