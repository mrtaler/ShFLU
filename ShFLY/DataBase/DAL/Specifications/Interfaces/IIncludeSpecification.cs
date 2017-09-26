using System;
using System.Collections.Generic;
using System.Linq;

namespace ShFLY.DataBase.DAL.Specifications.Interfaces
{
    public interface IIncludeSpecification<T>
    {
        List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; }
        void AddInclude(Func<IQueryable<T>, IQueryable<T>> includeExpression);
    }
}
