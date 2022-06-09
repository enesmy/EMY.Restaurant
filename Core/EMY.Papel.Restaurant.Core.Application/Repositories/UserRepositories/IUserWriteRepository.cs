using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.UserRepositories
{
    public interface IUserWriteRepository : IWriteRepository<User>
    {
       
        Task<ResultModel<User>> ChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer);
        Task<bool> CheckRoleIsExist(string userID, string formName, AuthType type);
        Task DeActivate(string userID, string deactivaterRef);

    }
}
