using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ShFLY.DataBase.DAL.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisifiedBy();
    }
}
