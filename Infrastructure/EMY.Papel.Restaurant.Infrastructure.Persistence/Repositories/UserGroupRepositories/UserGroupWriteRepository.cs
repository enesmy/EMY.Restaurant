using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories
{
    public class UserGroupWriteRepository : WriteRepository<UserGroup>, IUserGroupWriteRepository
    {
        public UserGroupWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
