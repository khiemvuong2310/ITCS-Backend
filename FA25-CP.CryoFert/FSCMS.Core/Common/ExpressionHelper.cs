using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Common
{
    /// <summary>
    /// Helper class for expression visitor to combine and manipulate expressions
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// Combines two filter expressions with an AND operation.
        /// </summary>
        public static Expression<Func<T, bool>> CombineFilters<T>(
            Expression<Func<T, bool>> filter1,
            Expression<Func<T, bool>> filter2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(filter1.Parameters[0], parameter);
            var left = leftVisitor.Visit(filter1.Body) ?? filter1.Body;

            var rightVisitor = new ReplaceExpressionVisitor(filter2.Parameters[0], parameter);
            var right = rightVisitor.Visit(filter2.Body) ?? filter2.Body;

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter);
        }
    }

    /// <summary>
    /// Helper class for expression visitor to replace parameters in expressions
    /// </summary>
    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceExpressionVisitor"/> class.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        /// <inheritdoc/>
        public override Expression? Visit(Expression? node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }

}
