using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.DataBase.DAL.Specifications.Interfaces;
using System.Linq.Expressions;

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
            var leftExpression = left.IsSatisifiedBy();
            var rightExpression = right.IsSatisifiedBy();

            var parameter = leftExpression.Parameters.Single();
            var body = Expression.OrElse(leftExpression.Body, SpecificationParameterRebinder.ReplaceParameter(rightExpression.Body, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
