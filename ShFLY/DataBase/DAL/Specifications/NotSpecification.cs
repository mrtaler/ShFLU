using System;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace ShFLY.DataBase.DAL.Specifications
{
    internal class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public Expression<Func<T, bool>> IsSatisifiedBy()
        {
            var expression = this.specification.IsSatisifiedBy();

            var parameter = expression.Parameters[0];
            var body = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
