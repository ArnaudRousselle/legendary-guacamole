namespace LegendaryGuacamole.WebApi.Extensions;

public static class EnumerableExtensions
{
    public static int FindIndexWithPredicate<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        for (var i = 0; i < enumerable.Count(); i++)
        {
            if (predicate(enumerable.ElementAt(i)))
                return i;
        }
        return -1;
    }
}