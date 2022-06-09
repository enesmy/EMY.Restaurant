using EMY.Papel.Restaurant.Core.Application.Repositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public WriteRepository(DbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<int> AddAsync(TEntity entity, Guid creater)
        {
            await Table.AddAsync(entity);
            return await SaveChangesAsync(default);
        }

        public async Task<int> AddRangeAsync(IList<TEntity> entities, Guid creater)
        {
            await Table.AddRangeAsync(entities);
            return await SaveChangesAsync(default);
        }

        public async Task<int> RemoveAsync(TEntity entity, Guid remover)
        {
#if DEBUG
            Table.Remove(entity);
            return await SaveChangesAsync(default);
#else
            ((BaseEntity)entity).IsDeleted = true;  
            return await UpdateAsync(entity);
#endif
        }

        public Task<int> RemoveAsync(Guid id, Guid remover)
        {
            var entity = Table.Find(id);
            return RemoveAsync(entity, remover);
        }

        public async Task<int> RemoveRangeAsync(IList<Guid> IDs, Guid remover)
        {
            List<TEntity> entities = new List<TEntity>();
            foreach (var id in IDs)
            {
                var entity = await Table.FindAsync(id);
                entities.Add(entity);
            }
            return await RemoveRangeAsync(entities, remover);
        }

        public async Task<int> RemoveRangeAsync(IList<TEntity> entities, Guid remover)
        {


#if DEBUG
            Table.RemoveRange(entities);
            return await SaveChangesAsync(default);
#else
            entities.ToList().ForEach(o => o.IsDeleted = true);  
            return await UpdateRangeAsync(entities);
#endif
        }

        public async Task<int> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate, Guid remover)
        {
            var entities = Table.Where(predicate);
            return await RemoveRangeAsync(await entities.ToListAsync(), remover);
        }

        public async Task<int> UpdateAsync(TEntity entity, Guid updater)
        {
            Table.Update(entity);
            return await SaveChangesAsync(default);
        }

        public async Task<int> UpdateRangeAsync(IList<TEntity> entities, Guid updater)
        {
            Table.UpdateRange(entities);
            return await SaveChangesAsync(default);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

    }
}
