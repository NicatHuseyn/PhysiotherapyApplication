using Microsoft.EntityFrameworkCore;

namespace PhysiotherapyApplication.Application.Paging;

/// <summary>
/// Provides extension methods for pagination functionality
/// </summary>
public static class PaginationExtension
{
    /// <summary>
    /// Asynchronously creates a Pagination object from an IQueryable source
    /// </summary>
    /// <typeparam name="T">The type of elements in the source</typeparam>
    /// <param name="source">The IQueryable source to paginate</param>
    /// <param name="index">The zero-based page index</param>
    /// <param name="size">The number of items per page</param>
    /// <param name="cancellationToken">A token to cancel the operation if needed</param>
    /// <returns>A Task representing the asynchronous operation, containing a Pagination object</returns>
    public static async Task<Pagination<T>> ToPaginationAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default
        )
    {
        var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source.Skip(index*size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        Pagination<T> list = new Pagination<T>()
        {
            Index = index,
            Size = size,
            Items = items,
            PageCount = (int)Math.Ceiling(count/(double)size)
        };

        return list;
    }


    // this is not async
    public static Pagination<T> ToPagination<T>(
            this IQueryable<T> sources,
            int index,
            int size
            )
    {
        int count = sources.Count();
        List<T> items = sources.Skip(index * size).Take(size).ToList();

        Pagination<T> list = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            PageCount = (int)Math.Ceiling(count / (double)size)
        };

        return list;
    }
}
