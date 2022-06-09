using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories
{
    public interface IMenuReadRepository : IReadRepository<Menu>
    {
        IList<Menu> GetMenuFromCategory(Guid guid);
        IList<Menu> GetAllMenu();
    }
}
