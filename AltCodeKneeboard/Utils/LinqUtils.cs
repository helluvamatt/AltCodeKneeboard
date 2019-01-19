using System;
using System.Collections.Generic;
using System.Linq;

namespace AltCodeKneeboard.Utils
{
    internal static class LinqUtils
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;
            foreach (var item in source)
            {
                if (bucket == null) bucket = new T[size];
                bucket[count++] = item;
                if (count < size) continue;
                yield return bucket;
                bucket = null;
                count = 0;
            }
            if (bucket != null && count > 0) yield return bucket.Take(count);
        }

        public static IEnumerable<TResult> SelectWithIndex<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> callback)
        {
            int index = 0;
            foreach (var item in source)
            {
                yield return callback(item, index);
                index++;
            }
        }
    }
}
