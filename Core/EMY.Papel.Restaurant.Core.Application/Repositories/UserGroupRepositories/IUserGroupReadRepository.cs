using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories
{
    public interface IUserGroupReadRepository : IReadRepository<UserGroup>
    {
        List<UserGroupRole> GetUserGroupRolesFromUserGroup(string userGroupID);
    }
}
