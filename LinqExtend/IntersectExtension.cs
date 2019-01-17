using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  LinqExtensions
{
    public static class IntersectExtension
    {
        public static IEnumerable<T> Intersect<T, V>(this IEnumerable<T> source,IEnumerable<T> secondSource, Func<T, V> keySelector)
        {
            return source.Intersect(secondSource,Equality<T>.CreateComparer(keySelector));
        }
        public static IEnumerable<T> Intersect<T, V>(this IEnumerable<T> source, IEnumerable<T> secondSource, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Intersect(secondSource,Equality<T>.CreateComparer(keySelector, comparer));
        }
    }
}
