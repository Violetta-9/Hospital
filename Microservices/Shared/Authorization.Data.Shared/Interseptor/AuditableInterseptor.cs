using Authorization.Data_Domain.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Data.Shared.Interseptor
{
    public class AuditableInterseptor:SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AuditProperty(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AuditProperty(eventData);

            return base.SavingChanges(eventData, result);
        }
        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            AuditProperty(eventData);

            base.SaveChangesFailed(eventData);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
                                                         CancellationToken cancellationToken = new())
        {
            AuditProperty(eventData);

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
       
        private void AuditProperty(DbContextEventData eventData)
        {
            Console.WriteLine(eventData);
            Console.WriteLine(eventData);

            if (eventData.Context == null)
                return;
            var date= DateTimeOffset.Now;
            var addOrModifEntities = eventData.Context.ChangeTracker.Entries<IAudientable>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();
            foreach(var entity in addOrModifEntities ) 
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.RowCreatedTimestamp= date;

                }
                entity.Entity.LastRowModificationTimestamp= date;
            }

        }
    }
}
