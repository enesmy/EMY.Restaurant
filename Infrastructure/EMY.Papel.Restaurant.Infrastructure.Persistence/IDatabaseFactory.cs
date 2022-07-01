using EMY.Papel.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Core.Application.Repositories.UserRepositories;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence
{
    public interface IDatabaseFactory
    {
        IMailListReadRepository MailListRead { get; set; }
        IMailListWriteRepository MailListWrite{ get; set; }

          public IMenuCategoryReadRepository MenuCategoryRead{ get; set; }
          public IMenuCategoryWriteRepository MenuCategoryWrite{ get; set; }
        
          public IMenuReadRepository MenuRead{ get; set; }
          public IMenuWriteRepository MenuWrite{ get; set; }
        
          public IOrderItemReadRepository OrderItemRead{ get; set; }
          public IOrderItemWriteRepository OrderItemWrite{ get; set; }
        
          public IOrderReadRepository OrderRead{ get; set; }
          public IOrderWriteRepository OrderWrite{ get; set; }
        
          public IPasswordHistoryReadRepository PasswordHistory{ get; set; }
          public IPasswordHistoryWriteRepository PasswordHistoryWrite{ get; set; }
        
          public IPhotoReadRepository PhotoRead{ get; set; }
          public IPhotoWriteRepository PhotoWrite{ get; set; }
        
          public IReservationReadRepository ReservationRead{ get; set; }
          public IReservationWriteRepository ReservationWrite{ get; set; }
        
          public IUserGroupReadRepository UserGroupRead{ get; set; }
          public IUserGroupWriteRepository UserGroupWrite{ get; set; }        

          public IUserGroupRoleReadRepository UserGroupRoleRead{ get; set; }
          public IUserGroupRoleWriteRepository UserGroupRoleWrite{ get; set; }
        
          public IUserReadRepository UserRead{ get; set; }
          public IUserWriteRepository UserWrite{ get; set; }

        
    }
}
