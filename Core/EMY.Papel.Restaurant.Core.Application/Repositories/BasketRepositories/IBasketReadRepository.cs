using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories
{
    public interface IBasketReadRepository : IReadRepository<Basket>
    {
        BasketStatsViewModel BasketStats();
    }
}
