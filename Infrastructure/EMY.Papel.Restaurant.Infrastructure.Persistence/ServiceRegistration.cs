using EMY.Papel.Restaurant.Core.Application.Abstract;
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
using EMY.Papel.Restaurant.Infrastructure.Persistence.Concrete;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Contexts;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.BasketRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MenuCategoryRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.MenuRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.PhotoRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories;
using EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {


            services.AddDbContext<EMYRestaurantDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<DbContext, EMYRestaurantDbContext>();
            services.AddPersistanceServicesReads();
            services.AddPersistanceServicesWrites();
            services.AddScoped<IEmailService, EmailManager>();
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();

        }
        public static void AddPersistanceServicesReads(this IServiceCollection services)
        {
            services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IMailListReadRepository, MailListReadRepository>();
            services.AddScoped<IMenuCategoryReadRepository, MenuCategoryReadRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IPhotoReadRepository, PhotoReadRepository>();
            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IUserGroupReadRepository, UserGroupReadRepository>();
            services.AddScoped<IUserGroupRoleReadRepository, UserGroupRoleReadRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IPasswordHistoryReadRepository, PasswordHistoryReadRepository>();
        }
        public static void AddPersistanceServicesWrites(this IServiceCollection services)
        {
            services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IMailListWriteRepository, MailListWriteRepository>();
            services.AddScoped<IMenuCategoryWriteRepository, MenuCategoryWriteRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IPhotoWriteRepository, PhotoWriteRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();
            services.AddScoped<IUserGroupWriteRepository, UserGroupWriteRepository>();
            services.AddScoped<IUserGroupRoleWriteRepository, UserGroupRoleWriteRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IPasswordHistoryWriteRepository, PasswordHistoryWriteRepository>();
        }

    }
}
