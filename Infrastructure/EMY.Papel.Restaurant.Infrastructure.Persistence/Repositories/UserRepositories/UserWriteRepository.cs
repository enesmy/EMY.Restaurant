using EMY.Papel.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserRepositories
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        IUserReadRepository _userReadRepository;
        IUserGroupWriteRepository _userGroupWriteRepository;
        IPasswordHistoryReadRepository _passwordHistoryReadRepository;
        IPasswordHistoryWriteRepository _passwordHistoryWriteRepository;
        IUserGroupRoleReadRepository _userGroupRoleReadRepository;

        public UserWriteRepository(DbContext context, IUserReadRepository userReadRepository, IUserGroupWriteRepository userGroupWriteRepository, IPasswordHistoryReadRepository passwordHistoryReadRepository, IPasswordHistoryWriteRepository passwordHistoryWriteRepository, IUserGroupRoleReadRepository userGroupRoleReadRepository) : base(context)
        {
            _userReadRepository = userReadRepository;
            _userGroupWriteRepository = userGroupWriteRepository;
            _passwordHistoryReadRepository = passwordHistoryReadRepository;
            _passwordHistoryWriteRepository = passwordHistoryWriteRepository;
            _userGroupRoleReadRepository = userGroupRoleReadRepository;
        }

        public async Task<ResultModel<User>> ChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer)
        {
            var result = await _userReadRepository.CanIChangePassword(userID, newPassword, hiddenQuestionAnswer);
            if (result.IsSuccess)
            {
                result.Value.PasswordStored = newPassword;
                await _passwordHistoryWriteRepository.AddAsync(new PasswordHistory(userID, newPassword), userID);
                await UpdateAsync(result.Value, userID);
                result.Message = "Password is changed!";
            }
            return result;
        }

        public async Task<ResultModel<User>> CheckLoginuser(string userName, string password)
        {
            ResultModel<User> result = new ResultModel<User>() { IsSuccess = false };

            var usrs = Table.Where(o => o.IsActive && !o.IsDeleted && o.UserName.ToLower() == userName.ToLower());
            if (usrs.Count() == 0)
            {
                result.Message = "User name is not found!";
                return result;
            }

            foreach (var user in usrs)
            {
                if (user.PasswordControl(password))
                {
                    if (user.IsLocked && user.LockedTime < DateTime.Now.AddMinutes(-Configuration.LockedMinutes))
                    {
                        var diff = DateTime.Now.AddMinutes(-Configuration.LockedMinutes) - user.LockedTime;
                        result.Message = $"Please {diff.Minutes} minutes after re try! Because this user is forced!";
                        return result;
                    }
                    user.Password = "";
                    result.Value = user;
                    result.IsSuccess = true;
                    result.Message = $"Welcome {user.Name} {user.LastName}";
                    return result;
                }
            }

            result.Message = "Password is not correct!";
            usrs.ToList().ForEach((usr) =>
            {
                if (!usr.IsLocked && usr.WrongForceCount + 1 >= Configuration.MaxWrongTryCount && usr.LastWrongTryingTime < DateTime.Now.AddMinutes(-Configuration.LockedMinutes))
                {
                    usr.WrongForceCount = 0;
                    usr.LastWrongTryingTime = DateTime.Now;
                    usr.LockedTime = DateTime.Now;
                    usr.IsLocked = true;
                    Table.Update(usr);
                    result.Message = $"Please {Configuration.LockedMinutes} minutes after re try! Because this user is forced and now `Locked!´" + Environment.NewLine + $"You tried {Configuration.MaxWrongTryCount} times wrong!";
                }
                else
                {
                    usr.WrongForceCount++;
                    usr.LastWrongTryingTime = DateTime.Now;
                    Table.Update(usr);
                }
            });

            return result;

        }

        public async Task<bool> CheckRoleIsExist(string userID, string formName, AuthType type)
        {
            var roles = await GetAllRoles(userID);
            string searchRole = AuthTypeHelper.GetAuthCode(formName, type);
            return roles.Contains(searchRole);
        }

        public async Task DeActivate(string userID, string DeactivaterRef)
        {
            var user = await Table.FindAsync(Guid.Parse(userID));
            if (user == null) throw new NullReferenceException("user is not exist!");

            if (!user.IsActive) throw new ArgumentException("user already deactivated");
            user.IsActive = false;
            await UpdateAsync(user, Guid.Parse(DeactivaterRef));
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var res = Table.Where(o => !o.IsDeleted);
            return res;
        }

        public async Task<IEnumerable<string>> GetAllRoles(string userID)
        {
            var res = await GetAuthorizes(userID);
            var result = res.Select(o => AuthTypeHelper.GetAuthCode(o.FormName, o.AuthorizeType)).AsEnumerable();
            return result;
        }

        public async Task<IEnumerable<UserGroupRole>> GetAuthorizes(string userID)
        {
            var roleGroup = await GetRoleGroup(userID);
            if (roleGroup != null)
            {
                var result = _userGroupRoleReadRepository.GetWhere(o => !o.IsDeleted && o.UserGroupID == roleGroup.UserGroupID, false);
                return result;
            }
            else
                return null;
        }

        public async Task<User> GetByuserID(string userID)
        {
            var user = await Table.FindAsync(Guid.Parse(userID));
            if (!user.IsDeleted) return user;
            else return null;
        }

        public async Task<User> GetuserByuserName(string userName)
        {
            var user = Table.Where(o => !o.IsDeleted && o.UserName == userName);
            return user.FirstOrDefault();
        }

        public async Task<UserGroup> GetRoleGroup(string userID)
        {
            var user = await Table.FindAsync(Guid.Parse(userID));
            if (user.IsDeleted) return null;

            var usergroup = await _userGroupWriteRepository.Table.FindAsync(user.UserGroupID);
            return usergroup;
        }

        public async Task SetRoleGroup(string userID, string RoleGroupID, string SetterID)
        {
            var user = await GetByuserID(userID);
            if (user.UserGroupID == Guid.Parse(RoleGroupID)) return;

            user.UserGroupID = Guid.Parse(RoleGroupID);
            await UpdateAsync(user, Guid.Parse(SetterID));



        }
    }
}
