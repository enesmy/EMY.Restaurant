﻿using EMY.Papel.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.BasketRepositories
{
    public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
    {
        public BasketReadRepository(DbContext context) : base(context)
        {
        }

        public BasketStatsViewModel BasketStats()
        {
            
            var today = GetWhere(o =>
            o.CreatedAt.Date == DateTime.Today.Date &&
            o.IsAuthorized != Core.Domain.Common.BasketAuthorizeStatus.Pending &&
            o.IsAuthorized != Core.Domain.Common.BasketAuthorizeStatus.Rejected
            , false);
            var yesterday = GetWhere(o =>
        o.CreatedAt.Date == DateTime.Today.AddDays(-1) &&
        o.IsAuthorized != Core.Domain.Common.BasketAuthorizeStatus.Pending &&
        o.IsAuthorized != Core.Domain.Common.BasketAuthorizeStatus.Rejected
        , false);

            
            var TodayBaskets = today.Count();
            decimal TodayBasketPrice = today.Sum(o => o.AfterDiscountPrice);


            int YesterdayBaskets = yesterday.Count();
            decimal YesterdayBasketPrice = yesterday.Sum(o => o.AfterDiscountPrice);


            var basketStats = new BasketStatsViewModel()
            {
                TodayBaskets = TodayBaskets,
                TodayBasketPrice = TodayBasketPrice,
                YesterdayBaskets = YesterdayBaskets,
                YesterdayBasketPrice = YesterdayBasketPrice
            };
            return basketStats;
        }
    }
}
