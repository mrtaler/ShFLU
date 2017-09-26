using System;
using System.Linq.Expressions;

using ShFLY.DataBase.DAL.Specifications.Interfaces;

namespace ShFLY.DataBase.DAL.Specifications
{
    public class ExpressionSpecification<T> : ISpecification<T>
    {
        private Expression<Func<T, bool>> expression;
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
        }

        public Expression<Func<T, bool>> IsSatisifiedBy()
        {
            return this.expression;
        }
    }
}
