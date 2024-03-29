﻿namespace My2DWorldServer.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEachCustom<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T entity in enumerable)
                action(entity);
        }

        public static async Task ForEachAsyncCustom<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (T entity in enumerable)
                await action(entity);
        }
    }
}
