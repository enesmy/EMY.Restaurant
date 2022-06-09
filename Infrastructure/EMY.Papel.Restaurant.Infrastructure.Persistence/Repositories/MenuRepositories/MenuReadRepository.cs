using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MenuRepositories
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(DbContext context) : base(context)
        {
        }

        public IList<Menu> GetAllMenu()
        {
            return GetWhere(o => !o.IsDeleted).ToList();
        }

        public IList<Menu> GetMenuFromCategory(Guid guid)
        {
            return GetWhere(o => o.MenuCategoryID == guid && !o.IsDeleted).ToList();
        }
    }
}
