namespace Crosscutting.Extensions;

public static class CollectionExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        if (source == null)
            return true;
        return !source.Any();
    }

    public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> source)
    {
        return source?.Where(x => x != null);
    }
}