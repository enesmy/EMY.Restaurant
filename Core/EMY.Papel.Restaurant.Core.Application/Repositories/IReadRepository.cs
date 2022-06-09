using EMY.Papel.Restaurant.Core.Domain.Entities;
using System.Linq.Expressions;

namespace EMY.Papel.Restaurant.Core.Application.Repositories
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll(bool tracking = true);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
        TEntity? GetById(Guid id);
        TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    }
}
