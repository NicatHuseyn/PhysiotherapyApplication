using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Domain.Entities.Common;
using System.Collections;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;

namespace PhysiotherapyApplication.Persistence.Repositories.BaseRepository;

/// <summary>
/// Implements the IRepository interface using Entity Framework Core.
/// Provides CRUD operations, pagination, and soft delete functionality for entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity this repository manages</typeparam>
/// <typeparam name="TContext">The type of DbContext used for database operations</typeparam>
public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    where TContext : IdentityDbContext
{
    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity with updated properties (e.g., generated ID)</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreateDate = DateTime.UtcNow;
        await context.AddAsync(entity);
        return entity;
    }


    /// <summary>
    /// Asynchronously adds multiple entities to the database in a single operation.
    /// </summary>
    /// <param name="entities">Collection of entities to add</param>
    /// <returns>The collection of added entities with updated properties</returns>
    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.CreateDate = DateTime.UtcNow;
        await context.AddRangeAsync(entities);
        return entities;
    }

    /// <summary>
    /// Asynchronously checks if any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">Optional condition to check. If null, checks if any entity exists</param>
    /// <param name="withDeleted">If true, includes soft-deleted entities in the check</param>
    /// <param name="enableTracking">If true, enables Entity Framework change tracking</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>True if matching entities exist, false otherwise</returns>
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (withDeleted)
            query = query.IgnoreQueryFilters();

        if (predicate != null)
            query = query.Where(predicate);

        return query.AnyAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously deletes an entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    /// <returns>The deleted entity</returns>
    public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
    {
        await SetEntityAsDeletedAsync(entity, permanent);
        return entity;
    }

    /// <summary>
    /// Asynchronously deletes multiple entities from the database in a single operation.
    /// </summary>
    /// <param name="entities">Collection of entities to delete</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    /// <returns>The collection of deleted entities</returns>
    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
    {
        await SetEntityAsDeletedAsync(entities, permanent);
        return entities;
    }

    /// <summary>
    /// Asynchronously retrieves a single entity based on the specified predicate with optional include expressions.
    /// </summary>
    /// <param name="predicate">Condition to filter the entity</param>
    /// <param name="include">Optional navigation property inclusion using Include/ThenInclude</param>
    /// <param name="withDeleted">If true, includes soft-deleted entities in the query</param>
    /// <param name="enableTracking">If true, enables Entity Framework change tracking</param>
    /// <param name="cancellationToken">Token to cancel the operation if needed</param>
    /// <returns>The entity matching the predicate or null if not found</returns>
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        if (include is not null)
            queryable = include(queryable);

        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();

        if (predicate is not null)
            queryable = queryable.Where(predicate);

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves a paginated list of entities based on the specified criteria.
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
    public async Task<Pagination<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query();

        if (!enableTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        if (withDeleted)
            query = query.IgnoreQueryFilters();

        if (predicate != null)
            query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToPaginationAsync(index, size, cancellationToken);

        return await query.ToPaginationAsync(index, size, cancellationToken);

    }

    /// <summary>
    /// Provides a queryable representation of the entity set.
    /// </summary>
    /// <returns>An IQueryable<TEntity> that can be used to compose LINQ queries</returns>
    public IQueryable<TEntity> Query() => context.Set<TEntity>();

    /// <summary>
    /// Asynchronously updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity with updated values</param>
    /// <returns>The updated entity</returns>
    public TEntity Udpated(TEntity entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        context.Update(entity);
        return entity;
    }

    /// <summary>
    /// Asynchronously updates multiple entities in the database in a single operation.
    /// </summary>
    /// <param name="entities">Collection of entities to update</param>
    /// <returns>The collection of updated entities</returns>
    public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.UpdateDate = DateTime.UtcNow;

        context.UpdateRange(entities);
        return entities;
    }


    // Helper methods

    /// <summary>
    /// Sets an entity as deleted, either by soft delete or permanent delete.
    /// </summary>
    /// <param name="entity">The entity to be deleted</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
    {
        if (!permanent)
        {
            //checking the entity's one to one relationship
            CheckHasEntityHaveOneToOneRelation(entity);
            await setEntityAsSoftDeletedAsync(entity);
        }
        else
        {
            context.Remove(entity);
        }
    }

    /// <summary>
    /// Checks if the entity has a one-to-one relationship that could cause issues with soft delete.
    /// </summary>
    /// <param name="entity">The entity to check</param>
    private void CheckHasEntityHaveOneToOneRelation(TEntity entity)
    {
        bool hasEntityHaveOneToOneRelation =
            context.Entry(entity)
            .Metadata.GetForeignKeys()
            .All(
                x =>
                x.DependentToPrincipal?.IsCollection == true
                || x.PrincipalToDependent?.IsCollection == true
                || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
                ) == false;

        if (hasEntityHaveOneToOneRelation)
        {
            throw new InvalidOperationException("Entity has one-to-one relationship. Soft Delete causes problems if you try to create entry again by same foreign key");
        }
    }

    /// <summary>
    /// Performs a soft delete on an entity and its related entities.
    /// </summary>
    /// <param name="entity">The entity to be soft deleted</param>
    private async Task setEntityAsSoftDeletedAsync(IEntityTimeStamps entity)
    {
        // stop the method if the entity is due to be deleted
        if (entity.DeleteDate.HasValue)
            return;
        entity.DeleteDate = DateTime.UtcNow;

        var navigations = context
            .Entry(entity)
            .Metadata.GetNavigations()
              .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
              .ToList();

        foreach (INavigation? navigation in navigations)
        {
            if (navigation.TargetEntityType.IsOwned())
                continue;
            if (navigation.PropertyInfo is null)
                continue;

            object? navValue = navigation.PropertyInfo.GetValue(entity);

            if (navigation.IsCollection)
            {
                if (navValue == null)
                {
                    IQueryable query = context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();

                    navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToListAsync();

                    if (navValue == null)
                        continue;
                }

                foreach (IEntityTimeStamps navValueItem in (IEnumerable)navValue)
                    await setEntityAsSoftDeletedAsync(navValueItem);
            }
            else
            {
                if (navValue == null)
                {
                    IQueryable query = context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                    navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                        .FirstOrDefaultAsync();
                    if (navValue == null)
                        continue;
                }

                await setEntityAsSoftDeletedAsync((IEntityTimeStamps)navValue);
            }
        }
        context.Update(entity);
    }

    /// <summary>
    /// Creates a query to load related entities, filtering out soft-deleted entities.
    /// </summary>
    /// <param name="query">The base query</param>
    /// <param name="navigationPropertyType">The type of the navigation property</param>
    /// <returns>A queryable filtered to exclude soft-deleted entities</returns>
    protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)
    {
        Type queryProviderType = query.Provider.GetType();
        MethodInfo createQueryMethod =
            queryProviderType
                .GetMethods()
                .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                ?.MakeGenericMethod(navigationPropertyType)
            ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
        var queryProviderQuery =
            (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
        return queryProviderQuery.Where(x => !((IEntityTimeStamps)x).DeleteDate.HasValue);
    }

    /// <summary>
    /// Sets multiple entities as deleted, either by soft delete or permanent delete.
    /// </summary>
    /// <param name="entities">The collection of entities to be deleted</param>
    /// <param name="permanent">If true, performs a permanent delete; if false, performs a soft delete</param>
    protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
    {
        foreach (TEntity entity in entities)
        {
            await SetEntityAsDeletedAsync(entity, permanent);
        }
    }
}
