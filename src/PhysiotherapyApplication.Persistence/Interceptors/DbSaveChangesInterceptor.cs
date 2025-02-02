using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Persistence.Interceptors;

public class DbSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly static Dictionary<EntityState, Action<DbContext, IEntityTimeStamps>> _behaviors = new()
    {
        { EntityState.Added, AddDataBehavior},
        { EntityState.Modified, UpdateDataBehavior},
        { EntityState.Deleted, DeleteDataBehavior},
    };


    private static void AddDataBehavior(DbContext context, IEntityTimeStamps entityTimeStamps)
    {
        entityTimeStamps.CreateDate = DateTime.UtcNow;
        context.Entry(entityTimeStamps).Property(e => e.UpdateDate).IsModified = false;
    }

    private static void UpdateDataBehavior(DbContext context, IEntityTimeStamps entityTimeStamps)
    {
        entityTimeStamps.UpdateDate = DateTime.UtcNow;
        context.Entry(entityTimeStamps).Property(e => e.DeleteDate).IsModified = false;
    }

    private static void DeleteDataBehavior(DbContext context, IEntityTimeStamps entityTimeStamps)
    {
        entityTimeStamps.DeleteDate = DateTime.UtcNow;
        context.Entry(entityTimeStamps).Property(e => e.UpdateDate).IsModified = false;

        // for soft delete
        context.Entry(entityTimeStamps).State = EntityState.Modified;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context!.ChangeTracker.Entries<BaseEntity>();

        foreach (var entityEntry in dbContext)
        {
            if (entityEntry.Entity is not IEntityTimeStamps entityTimeStamps)
            {
                continue;
            }


            if (_behaviors.ContainsKey(entityEntry.State))
            {
                _behaviors[entityEntry.State](eventData.Context, entityTimeStamps);
            }
        }


        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
