using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories
{
    public class UserGroupRoleReadRepository : ReadRepository<UserGroupRole>, IUserGroupRoleReadRepository
    {
        public UserGroupRoleReadRepository(DbContext context) : base(context)
        {
        }
    }
}
