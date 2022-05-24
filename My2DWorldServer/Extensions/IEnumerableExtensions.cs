namespace My2DWorldServer.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEachCustom<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(T entity in enumerable)
                action(entity);
        }
    }
}
