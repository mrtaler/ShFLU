using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace TicketSaleCore.Models
{
    public interface IReadableRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                      int? skip = null,
           int? take = null);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                        int? skip = null,
            int? take = null);

        TEntity GetOne(
            ISpecification<TEntity> specification);

        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity GetById(
            object id);

        int GetCount(
            Expression<Func<TEntity, bool>> filter = null);

        bool GetExist(
            Expression<Func<TEntity, bool>> filter = null);

        IEnumerable<TEntity> GetLocal();
    }
}
