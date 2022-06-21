using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
        BasketStatsViewModel BasketStats();
    }
}
