using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories
{
    public class UserGroupReadRepository : ReadRepository<UserGroup>, IUserGroupReadRepository
    {
        IUserGroupRoleReadRepository _userGroupRoleReadRepository;
        public UserGroupReadRepository(DbContext context, IUserGroupRoleReadRepository userGroupRoleReadRepository) : base(context)
        {
            _userGroupRoleReadRepository = userGroupRoleReadRepository;
        }

        public List<UserGroupRole> GetUserGroupRolesFromUserGroup(string userGroupID)
        {
            return _userGroupRoleReadRepository.GetWhere(o => o.UserGroupID == userGroupID.ToGuid() && !o.IsDeleted, false).ToList();
        }

   
    }
}
