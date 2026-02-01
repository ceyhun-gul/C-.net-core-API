using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IBaseAuditEntity>> Behaviors = new()
    {
        {
            EntityState.Added, AddBehaviors
        },
        {
            EntityState.Modified, ModifiedBehaviors
        }
    };

    private static void AddBehaviors(DbContext context, IBaseAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
    }

    private static void ModifiedBehaviors(DbContext context, IBaseAuditEntity auditEntity)
    {
        context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
        auditEntity.Updated = DateTime.Now;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if (entityEntry.Entity is not IBaseAuditEntity auditEntity) continue;

            Behaviors[entityEntry.State](eventData.Context, auditEntity);

            // switch (entityEntry.State)
            // {
            //     case EntityState.Added:
            //         AddBehaviors(eventData.Context, auditEntity);
            //         break;
            //     case EntityState.Modified:
            //         ModifiedBehaviors(eventData.Context, auditEntity);
            //         break;
            // }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}