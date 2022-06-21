using EMY.Papel.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories
{
    public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
    {
        public OrderItemReadRepository(DbContext context) : base(context)
        {
        }
    }
}
