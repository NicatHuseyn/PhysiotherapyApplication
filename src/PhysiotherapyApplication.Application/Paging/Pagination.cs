namespace PhysiotherapyApplication.Application.Paging;

/// <summary>
/// Represents a paginated result set of items of type T
/// </summary>
/// <typeparam name="T">The type of items in the paginated result</typeparam>
public class Pagination<T>
{
    /// <summary>
    /// Gets or sets the current page index (0-based)
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Gets or sets the total number of items across all pages
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// Gets or sets the list of items on the current page
    /// </summary>
    public IList<T> Items { get; set; }

    /// <summary>
    /// Indicates whether there is a previous page
    /// </summary>
    public bool HasPrevious => Index > 0;

    /// <summary>
    /// Indicates whether there is a next page
    /// </summary>
    public bool HasNext => Index + 1 < PageCount;

    /// <summary>
    /// Initializes a new instance of the Pagination class with an empty list of items
    /// </summary>
    public Pagination()
    {
        Items = Array.Empty<T>();
    }
}
