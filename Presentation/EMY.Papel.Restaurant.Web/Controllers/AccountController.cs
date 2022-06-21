using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;
using EMY.Papel.Restaurant.Web.Statics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EMY.Papel.Restaurant.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserReadRepository _userReadRepository;
        IUserWriteRepository _userWriteRepository;
        IUserGroupReadRepository _userGroupReadRepository;
        IUserGroupWriteRepository _userGroupWriteRepository;
        IUserGroupRoleReadRepository _userGroupRoleReadRepository;
        IUserGroupRoleWriteRepository _userGroupRoleWriteRepository;

        const string ControllerName = "Account";

        public AccountController(IUserReadRepository userReadRepository, IUserGroupReadRepository userGroupReadRepository, IUserWriteRepository userWriteRepository, IUserGroupWriteRepository userGroupWriteRepository, IUserGroupRoleReadRepository userGroupRoleReadRepository, IUserGroupRoleWriteRepository userGroupRoleWriteRepository)
        {
            this.ViewBag.FormName = ControllerName;
            _userReadRepository = userReadRepository;
            _userGroupReadRepository = userGroupReadRepository;
            _userWriteRepository = userWriteRepository;
            _userGroupWriteRepository = userGroupWriteRepository;
            _userGroupRoleReadRepository = userGroupRoleReadRepository;
            _userGroupRoleWriteRepository = userGroupRoleWriteRepository;
        }




        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            UserLoginRequest login = new UserLoginRequest();
            return PartialView(login);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginRequest login)
        {
            await CheckAdminUser(await CheckAdminRole());

            var res = await _userReadRepository.CheckLoginProfile(login.UserName, login.Password);
            if (res.IsSuccess)
            {
                var loggedinUser = res.Value;

                List<Claim> mainClaim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, loggedinUser.GetFullName()),
                    new Claim(ClaimTypes.Name, loggedinUser.UserID.ToString())
                };

                List<UserGroupRole> authList = await _userReadRepository.GetAllRoles(loggedinUser.UserID);

                foreach (var role in authList)
                {
                    var rl = new Claim(ClaimTypes.Role, AuthTypeHelper.GetAuthCode(role.FormName, role.AuthorizeType));
                    mainClaim.Add(rl);
                }

                ClaimsIdentity MainIdentity = new ClaimsIdentity(mainClaim, SystemMainStatics.ClaimIdentity);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(MainIdentity);

                await HttpContext.SignInAsync(
                    SystemMainStatics.DefaultScheme,
                    userPrincipal,
                    new AuthenticationProperties { IsPersistent = login.RememberMe }
                    );
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = res.Message;
                ViewBag.Error = true;
                return PartialView(login);
            }


        }

        [Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme)]
        public async Task<IActionResult> LogOut()
        {
            if (HttpContext.Request.Cookies.Count > 0)
            {
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in siteCookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }
            await HttpContext.SignOutAsync(scheme: SystemMainStatics.DefaultScheme);
            return RedirectToAction("Login");
        }

        async Task<Guid> CheckAdminRole()
        {
#if DEBUG
            var userGroups = _userGroupReadRepository.GetWhere(o => !o.IsDeleted);
            var adminGroup = userGroups.FirstOrDefault(o => !o.IsDeleted && o.UserGroupCode == ControllerName);
            if (adminGroup == null)
            {
                adminGroup = new UserGroup()
                {
                    UserGroupCode = ControllerName,
                    UserGroupName = ControllerName,
                    UserGroupToolTip = "Admin Group has all authorizes!"
                };
                await _userGroupWriteRepository.AddAsync(adminGroup, Guid.Empty);
                var admingroupauthorize = new UserGroupRole()
                {
                    AuthorizeType = AuthType.Full,
                    FormName = ControllerName,
                    UserGroupID = adminGroup.UserGroupID
                };
                await _userGroupRoleWriteRepository.AddAsync(admingroupauthorize, Guid.Empty);

            }
            return adminGroup.UserGroupID;
#else
return Guid.Empty;
#endif

        }
        async Task CheckAdminUser(Guid adminGroupID)
        {
#if DEBUG
            var adminEnesMY = await _userReadRepository.GetAsync(o => o.UserName == "EnesMY" && !o.IsDeleted);
            if (adminEnesMY == null)
            {

                User adminProfile = new User()
                {
                    UserID = Guid.NewGuid(),
                    Email = "Enes@Globaldenim.mx",
                    HiddenQuestion = "WH?",
                    HiddenQuestionAnswer = "Coding",
                    IsActive = true,
                    LastName = "YILDIRIM",
                    UserName = "EnesMY",
                    Name = "ENES MURAT",
                    Notes = "DEBUGGING",
                    Password = "123456",
                    WrongForceCount = 0,
                    IsDeleted = false,
                    IsLocked = false,
                    UserGroupID = adminGroupID,
                    Phone = "",
                    UserStatus = UserStatus.Active,



                };
                await _userWriteRepository.AddAsync(adminProfile, Guid.Empty);

            }
#else
return;
#endif
        }

        [Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme)]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userReadRepository.GetByIdAsync(this.ActiveUserID());
            if (user == null) Redirect("/Account/Login");
            return View(user);
        }

        [Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme), HttpPost]
        public async Task<IActionResult> ChangeMyPassword(string newPassword, string hiddenQuestionAnswer)
        {
            ResultModel<User> me = await _userReadRepository.CanIChangePassword(this.ActiveUserID(), newPassword, hiddenQuestionAnswer);
            if (me.IsSuccess)
            {
                var result = await _userWriteRepository.ChangePassword(this.ActiveUserID(), newPassword, hiddenQuestionAnswer);
                if (result.IsSuccess)
                    return Ok("Password changed!");
                else return ValidationProblem(result.Message);
            }
            else
            {
                return ValidationProblem(me.Message);
            }
        }
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public async Task<IActionResult> UserList()
        {
            var userlist = _userReadRepository.GetWhere(o => !o.IsDeleted, false);
            var userGroups = _userGroupReadRepository.GetWhere(o => !o.IsDeleted, false);
            var departmentList =
                (from a in userGroups
                 select new SelectListItem
                 {
                     Value = a.UserGroupID.ToString(),
                     Text = a.UserGroupName,
                     Disabled = false,
                     Selected = false
                 }).ToList();
            ViewBag.userGroup = departmentList;
            return View(userlist);

        }
        [HttpGet]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public IActionResult Register()
        {
            ViewBag.Error = false;
            ViewBag.ErrorMessage = "";
            User usr = new User();
            return View(usr);
        }
        [HttpGet]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public async Task<IActionResult> RoleManager(string UserGroupID)
        {
            List<UserGroupRole> usergrouproles = _userGroupReadRepository.GetUserGroupRolesFromUserGroup(UserGroupID);
            ViewBag.UserGroupID = UserGroupID;
            var usergroup = await _userGroupReadRepository.GetByIdAsync(Guid.Parse(UserGroupID));
            if (usergroup == null)
                return Unauthorized();

            ViewBag.UserGroup = usergroup.GetGroupName();
            ViewBag.ToolTip = usergroup.UserGroupToolTip;
            return View(usergrouproles.ToList());
        }

        [HttpPost]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public async Task<IActionResult> Register(User user)
        {
            ViewBag.Error = true;
            User usr = await _userReadRepository.GetAsync(o => o.UserName == user.UserName && !o.IsDeleted) ?? new User();
            if (usr != null)
            {
                ViewBag.ErrorMessage = "This user name already registered!";
                return View(usr);
            }

            user.Password = "123456";
            user.IsActive = true;
            user.IsDeleted = false;
            await _userWriteRepository.UpdateAsync(user, this.ActiveUserID());

            return Redirect("UserList");
        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme, Roles = "AdminFull")]
        public async Task<IActionResult> Activate(string UserID)
        {
            var user = await _userReadRepository.GetByIdAsync(UserID.ToGuid());
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = true;

            await _userWriteRepository.UpdateAsync(user, this.ActiveUserID());
            return Redirect("UserList");

        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme, Roles = "AdminFull")]
        public async Task<IActionResult> DeActivate(string UserID)
        {
            var user = await _userReadRepository.GetByIdAsync(UserID.ToGuid());
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;

            await _userWriteRepository.UpdateAsync(user, this.ActiveUserID());
            return Redirect("UserList");

        }

        [HttpGet, Authorize(AuthenticationSchemes = SystemMainStatics.DefaultScheme, Roles = "AdminFull")]
        public async Task<IActionResult> ResetPassword(string UserID)
        {
            var user = await _userReadRepository.GetByIdAsync(Guid.Parse(UserID));
            await _userWriteRepository.ChangePassword(UserID.ToGuid(), "123456", user.HiddenQuestionAnswer);
            return Redirect("UserList");
        }
        [HttpPost]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public async Task<IActionResult> AddRole(string UserGroupID, string FormName, AuthType rollType)
        {
            var roles = _userGroupReadRepository.GetUserGroupRolesFromUserGroup(UserGroupID);
            var foundRole = roles.FirstOrDefault(o => !o.IsDeleted && o.FormName == FormName && o.AuthorizeType == rollType);
            if (foundRole != null) return BadRequest("Role is already exist!");
            else
            {
                UserGroupRole newauth = new UserGroupRole();
                newauth.UserGroupID = Guid.Parse(UserGroupID);
                newauth.FormName = FormName;
                newauth.AuthorizeType = rollType;
                await _userGroupRoleWriteRepository.AddAsync(newauth, this.ActiveUserID());
                return Ok("Role is added!");
            }

        }
        [HttpPost]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public async Task<IActionResult> DeleteRole(string UserGroupID, string FormName, AuthType rollType)
        {
            var roles = _userGroupReadRepository.GetUserGroupRolesFromUserGroup(UserGroupID);
            var deletedRole = roles.FirstOrDefault(o => !o.IsDeleted && o.FormName == FormName && o.AuthorizeType == rollType);
            if (deletedRole == null) return NotFound("Role is not found!");
            else
            {

                await _userGroupRoleWriteRepository.RemoveAsync(deletedRole.UserGrpoupRoleID, this.ActiveUserID());
                return Ok("Role Deleted!");
            }
        }


        [HttpGet]
        [EMY_ISINROLE(ControllerName, AuthType.Full)]
        public IActionResult UserGroups()
        {
            List<UserGroup> groups = _userGroupReadRepository.GetWhere(o => !o.IsDeleted, false).ToList();
            return View(groups);
        }

        [HttpPost]
        [EMY_ISINROLE("UserGroup", AuthType.Full)]
        public async Task<IActionResult> SaveUserGroup(UserGroup usergroup)
        {
            if (usergroup.UserGroupID != Guid.Empty)
            {
                var foundUserGroup = await _userGroupReadRepository.GetByIdAsync(usergroup.UserGroupID);
                if (foundUserGroup == null) return NotFound("User group does not exist!");

                foundUserGroup.UserGroupCode = usergroup.UserGroupCode;
                foundUserGroup.UserGroupName = usergroup.UserGroupName;
                foundUserGroup.UserGroupToolTip = usergroup.UserGroupToolTip;
                await _userGroupWriteRepository.UpdateAsync(foundUserGroup, this.ActiveUserID());
                return Ok($"User group is updated! ID Number:{usergroup.UserGroupID}");
            }
            else
            {
                usergroup.DefaultUserGroup = false;
                await _userGroupWriteRepository.AddAsync(usergroup, this.ActiveUserID());
                return Ok($"User group is added! ID Number:{usergroup.UserGroupID}");
            }
        }



        [HttpPost]
        [EMY_ISINROLE("UserGroup", AuthType.Full)]
        public async Task<IActionResult> RemoveUserGroup(string UserGroupID)
        {
            var usergroup = await _userGroupReadRepository.GetByIdAsync(Guid.Parse(UserGroupID));
            if (usergroup == null) return Unauthorized("User group not found!");
            if (usergroup.DefaultUserGroup) return NotFound("This user group is default user group!");
            await _userGroupWriteRepository.RemoveAsync(usergroup, this.ActiveUserID());
            return Ok("User group is deleted!");
        }
    }
}
