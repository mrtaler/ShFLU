using System;
using System.Linq.Expressions;

namespace ShFLY.DataBase.DAL.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisifiedBy();
    }
}
