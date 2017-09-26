using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TicketSaleCore.Models
{
    public interface IRepository<TEntity> : IReadableRepository<TEntity>
        where TEntity : class
    {
        void Create(TEntity entity, string createdBy = null);
        void Update(TEntity entity, string modifiedBy = null);
        void Delete(object id,      string deleteBy = null);
        void Delete(TEntity entity, string deleteBy = null);
    }
}
