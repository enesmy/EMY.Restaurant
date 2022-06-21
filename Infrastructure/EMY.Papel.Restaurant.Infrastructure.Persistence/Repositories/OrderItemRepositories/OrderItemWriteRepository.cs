using EMY.Papel.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories
{
    public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
    {
        public OrderItemWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
