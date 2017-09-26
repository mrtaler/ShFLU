using System;
using System.Linq;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace ShFLY.DataBase.DAL.Specifications
{
    internal class OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public Expression<Func<T, bool>> IsSatisifiedBy()
        {
            var leftExpression = this.left.IsSatisifiedBy();
            var rightExpression = this.right.IsSatisifiedBy();

            var parameter = leftExpression.Parameters.Single();
            var body = Expression.OrElse(leftExpression.Body, SpecificationParameterRebinder.ReplaceParameter(rightExpression.Body, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
