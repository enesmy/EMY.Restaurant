using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MenuCategoryRepositories
{
    public class MenuCategoryReadRepository : ReadRepository<MenuCategory>, IMenuCategoryReadRepository
    {
        public MenuCategoryReadRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<MenuCategory>> GetAllMenuCategoryWithMenus()
        {
            return await GetWhere(o => !o.IsDeleted).Include(o => o.Menus).ToListAsync();
        }
    }
}
