using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories
{
    public interface IMenuCategoryReadRepository : IReadRepository<MenuCategory>
    {
        Task<List<MenuCategory>> GetAllMenuCategoryWithMenus();
    }
}
