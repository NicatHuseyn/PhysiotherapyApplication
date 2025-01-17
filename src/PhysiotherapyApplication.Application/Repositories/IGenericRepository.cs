using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using PhysiotherapyApplication.Domain.Entities.Common;
using PhysiotherapyApplication.Application.Paging;

namespace PhysiotherapyApplication.Application.Repositories;

/// <summary>
/// Generic repository interface for entity operations with advanced querying capabilities
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from BaseEntity and implements IQuery</typeparam>
public interface IGenericRepository<TEntity>:IQuery<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves a single entity based on the specified predicate with optional include expressions
    /// </summary>
    /// <param name="predicate">Condition to filter the entity</param>
    /// <param name="include">Optional navigation property inclusion using Include/ThenInclude</param>
    /// <param name="withDeleted">If true, includes soft-deleted entities in the query</param>
    /// <param name="enableTracking">If true, enables Entity Framework change tracking</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>The entity matching the predicate or null if not found</returns>
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieves a paginated list of entities based on the specified criteria
    /// </summary>
    /// <param name="predicate">Condition to filter the entities</param>
    /// <param name="orderBy">Optional ordering expression</param>
    /// <param name="include">Optional navigation property inclusion using Include/ThenInclude</param>
    /// <param name="index">Page number (0-based)</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="withDeleted">If true, includes soft-deleted entities in the query</param>
    /// <param name="enableTracking">If true, enables Entity Framework change tracking</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>Paginated result containing entities and total count</returns>
    Task<Pagination<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Checks if any entity matches the specified predicate
    /// </summary>
    /// <param name="predicate">Optional condition to check. If null, checks if any entity exists</param>
    /// <param name="withDeleted">If true, includes soft-deleted entities in the check</param>
    /// <param name="enableTracking">If true, enables Entity Framework change tracking</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>True if matching entities exist, false otherwise</returns>
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity with updated properties (e.g., generated ID)</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the repository in a single operation
    /// </summary>
    /// <param name="entities">Collection of entities to add</param>
    /// <returns>The collection of added entities with updated properties</returns>
    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    /// <returns>The updated entity</returns>
    TEntity Udpated(TEntity entity);

    /// <summary>
    /// Updates multiple entities in the repository in a single operation
    /// </summary>
    /// <param name="entities">Collection of entities to update</param>
    /// <returns>The collection of updated entities</returns>
    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

    /// <summary>
    /// Deletes an entity from the repository
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    /// <returns>The deleted entity</returns>
    Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);

    /// <summary>
    /// Deletes multiple entities from the repository in a single operation
    /// </summary>
    /// <param name="entities">Collection of entities to delete</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    /// <returns>The collection of deleted entities</returns>
    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);
}
