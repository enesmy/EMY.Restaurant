using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories
{
    public class UserGroupRoleWriteRepository : WriteRepository<UserGroupRole>, IUserGroupRoleWriteRepository
    {
        public UserGroupRoleWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
