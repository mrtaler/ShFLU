using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;

using TicketSaleCore.Models;

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
                if (this.entities == null)
                {
                    this.entities = this.context.Set<TEntity>();
                }

                return this.entities;
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
            IQueryable<TEntity> query = this.Table;

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
            this.DbSet.Add(entity);
        }

        public void Update(TEntity entity, string modifiedBy = null)
        {
            this.DbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id, string deleteBy = null)
        {
            TEntity entity = this.DbSet.Find(id);
            this.Delete(entity);
        }

        public void Delete(TEntity entity, string deleteBy = null)
        {

            if (this.context.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            this.DbSet.Remove(entity);
        }

        #region
        public virtual IEnumerable<TEntity> GetAll(
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? skip = null,
             int? take = null)
        {
            return this.GetQueryable(null, orderBy, skip, take).ToList();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null)
        {
            var ret = this.GetQueryable(filter, orderBy, skip, take).ToList();
            return ret;
        }

        public virtual TEntity GetById(object id)
        {
            var ret = this.DbSet.Find(id);
            return ret;
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var ret = this.GetQueryable(filter).Count();
            return ret;
        }

        public virtual bool GetExist(Expression<Func<TEntity, bool>> filter = null)
        {
            var ret = this.GetQueryable(filter).Any();
            return ret;
        }

        public virtual TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var ret = this.GetQueryable(filter, orderBy).FirstOrDefault();
            return ret;
        }
       
        #endregion
        
        public virtual TEntity GetOne(ISpecification<TEntity> specification = null)
        {
            var query = this.DbSet.Where(specification.IsSatisifiedBy());
            return query.SingleOrDefault();
        }

        public IEnumerable<TEntity> GetLocal()
        {
            return this.DbSet.Local;
        }
    }
}
