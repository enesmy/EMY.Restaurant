using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EMY.Papel.Restaurant.Core.Application.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> AddAsync(TEntity entity, Guid creater);
        Task<int> AddRangeAsync(IList<TEntity> entitiesy, Guid creater);

        Task<int> UpdateAsync(TEntity entity,Guid updater);
        Task<int> UpdateRangeAsync(IList<TEntity> entities, Guid updater);


        Task<int> RemoveAsync(TEntity entity, Guid remover);
        Task<int> RemoveAsync(Guid id, Guid remover);
        Task<int> RemoveRangeAsync(IList<Guid> IDs, Guid remover);
        Task<int> RemoveRangeAsync(IList<TEntity> entities, Guid remover);
        Task<int> RemoveRangeAsync(Expression<Func<TEntity, bool>> predicate, Guid remover);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
