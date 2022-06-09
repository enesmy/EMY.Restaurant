using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MenuCategoryRepositories
{
    public class MenuCategoryWriteRepository : WriteRepository<MenuCategory>, IMenuCategoryWriteRepository
    {
        public MenuCategoryWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
