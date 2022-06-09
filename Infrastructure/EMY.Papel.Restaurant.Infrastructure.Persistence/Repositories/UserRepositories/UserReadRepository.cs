using EMY.Papel.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserRepositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        IPasswordHistoryReadRepository _passwordHistoryReadRepository;
        public UserReadRepository(DbContext context, IPasswordHistoryReadRepository passwordHistoryReadRepository) : base(context)
        {
            _passwordHistoryReadRepository = passwordHistoryReadRepository;
        }

        public async Task<ResultModel<User>> CanIChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer)
        {
            ResultModel<User> result = new ResultModel<User>();
            result.IsSuccess = false;

            //Password is empty?
            if (string.IsNullOrEmpty(newPassword))
            {
                result.Message = "Please write a password!";
                return result;
            }

            User prf = await GetByIdAsync(userID);

            //hidden answer is correct?

            if (prf.HiddenQuestionAnswer != hiddenQuestionAnswer)
            {
                result.Message = "Hidden answer is not correct!";
                return result;
            }

            var history = _passwordHistoryReadRepository.GetWhere(o => o.UserID == userID);
            if (history.OrderByDescending(o => o.PasswordHistoryID).Take(3).Count(o => o.PasswordHash == newPassword) > 0)
            {
                result.Message = "You are used in your history(Last 3 Password) this password!";
                return result;
            }
            result.Value = prf;
            result.Message = "You can use this password!";
            result.IsSuccess = true;
            return result;
        }

        public Task<ResultModel<User>> CheckLoginProfile(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserGroupRole>> GetAllRoles(Guid userID)
        {
            throw new NotImplementedException();
        }
    }
}
