using Authorization.Data_Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Shared.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data.Repository.Abstraction
{
    public interface IRepositoryBase<TEntity>
    {
            Task DeleteAsync(long id, CancellationToken cancellationToken = default);
            Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
            Task<TEntity> GetAsync(long id, CancellationToken cancellationToken = default);

            Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);
            Task<TEntity> InsertAsync(TEntity newEntity, CancellationToken cancellationToken = default);
            Task InsertRangeAsync(ICollection<TEntity> newEntity, CancellationToken cancellationToken = default);
            Task<TEntity> UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken = default);
            Task UpdateRangeAsync(ICollection<TEntity> entitiesToUpdate, CancellationToken cancellationToken = default);
            
            
        
    }
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : KeyedEntityBase
    {
        protected HospitalDbContext DbContext { get; }

        public RepositoryBase(HospitalDbContext dbContext)
        {
            DbContext = dbContext;
        }

        

        public Task<TEntity> GetAsync(long id, CancellationToken cancellationToken)
        {
            return DbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        }

       

        public Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TEntity>().ToArrayAsync(cancellationToken);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity newEntity, CancellationToken cancellationToken)
        {
            DbContext.Set<TEntity>().Add(newEntity);
            await DbContext.SaveChangesAsync(cancellationToken);

            return newEntity;
        }

        public virtual async Task InsertRangeAsync(ICollection<TEntity> entitiesToInsert,
                                                   CancellationToken cancellationToken)
        {
            foreach (var entity in entitiesToInsert) DbContext.Set<TEntity>().Add(entity);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken)
        {
            if (DbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                DbContext.Set<TEntity>().Attach(entityToUpdate);
            }

            DbContext.ChangeTracker.DetectChanges();
            await DbContext.SaveChangesAsync(cancellationToken);

            return entityToUpdate;
        }

        public virtual async Task UpdateRangeAsync(ICollection<TEntity> entitiesToUpdate,
                                                   CancellationToken cancellationToken)
        {
            if (!entitiesToUpdate.Any())
            {
                return;
            }

            foreach (var entity in entitiesToUpdate)
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbContext.Set<TEntity>().Attach(entity);
                }

            DbContext.Set<TEntity>().UpdateRange(entitiesToUpdate);

            await DbContext.SaveChangesAsync(cancellationToken);
        }



        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return DbContext.Set<TEntity>().AnyAsync(entity => entity.Id.Equals(id), cancellationToken);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(id);

            await DeleteAsync(entity, cancellationToken);
            
        }
        public virtual async Task DeleteAsync(TEntity entityToRemove, CancellationToken cancellationToken)
        {
            DbContext.Set<TEntity>().Remove(entityToRemove);

            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
