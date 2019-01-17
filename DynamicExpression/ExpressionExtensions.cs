﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicExpression
{
    public static class ExpressionExtensions
    {
        public static IEnumerable<TSource> WhereDynamic<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var result = ExpressionManager.ExpressionConvert(predicate);
            return source.Where(result.Compile());
        }

        public static IQueryable<TSource> WhereDynamic<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var result = ExpressionManager.ExpressionConvert(predicate);
            return source.Where(result);
        }

        public static Expression<Func<TSource, bool>> FilterNull<TSource>(this Expression<Func<TSource, bool>> predicate)
        {
            return ExpressionManager.ExpressionConvert(predicate);
        }
    }
}
