namespace PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;

/// <summary>
/// Defines a contract for entities that support custom querying capabilities
/// </summary>
/// <typeparam name="T">The type of entity this query interface is for</typeparam>
public interface IQuery<T>
{
    /// <summary>
    /// Provides a queryable representation of the entity set
    /// </summary>
    /// <returns>An IQueryable<T> that can be used to compose LINQ queries</returns>
    IQueryable<T> Query();
}
