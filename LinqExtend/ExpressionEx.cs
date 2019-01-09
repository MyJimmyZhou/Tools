using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqExtend
{
    public class ExpressionEx<T>
    {
        public static Expression<Func<T, bool>> CreateExpression(T model, Dictionary<string, ExpressionType> expressionTypes = null)
        {
            Expression<Func<T, bool>> expression = null;
            foreach (PropertyInfo p in model.GetType().GetProperties())
            {
                if (p.GetValue(model) == null) continue;
                ParameterExpression parameter = Expression.Parameter(p.GetType());;
                if (!expressionTypes.TryGetValue(p.Name, out ExpressionType expressionType)) expressionType = ExpressionType.Equal;
                Expression left = Expression.Property(parameter, p.Name);
                Expression right = Expression.Constant(p.GetValue(model), p.GetType());
                BinaryExpression binaryExpression = Expression.MakeBinary(expressionType, left, right);
                if(expression == null)
                {
                    expression = Expression.Lambda<Func<T, bool>>(binaryExpression, new ParameterExpression[] { }); ;
                }
                else
                {
                    expression = Expression.Lambda<Func<T, bool>>(binaryExpression, expression.Parameters);
                }
            }
            return expression;
        }
    }
}
