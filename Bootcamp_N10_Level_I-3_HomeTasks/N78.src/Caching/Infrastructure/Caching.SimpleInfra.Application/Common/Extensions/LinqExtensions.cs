using Caching.SimpleInfra.Application.Common.Querying;

namespace Caching.SimpleInfra.Application.Common.Extensions;

public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, FilterPagination paginationOptioins)
    {
        return source.Skip((int)((paginationOptioins.PageToken-1)*paginationOptioins.PageSize)).Take((int)paginationOptioins.PageSize);
    }
    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source, FilterPagination paginationOptions)
    {
        return source.Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize)).Take((int)paginationOptions.PageSize);
    }
}
