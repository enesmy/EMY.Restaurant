using EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories;
using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Repositories.ReservationRepositories
{
    public class ReservationReadRepository : ReadRepository<Reservation>, IReservationReadRepository
    {
        public ReservationReadRepository(DbContext context) : base(context)
        {
        }

        public List<Reservation> GetAuthorizedReservations()
        {
            return GetWhere(o => o.ConfirmationStatus == ReservationConfirmationStatus.Authorized && !o.IsDeleted).ToList();
        }

        public List<Reservation> GetReservations()
        {
            return GetWhere(o => !o.IsDeleted).ToList();
        }

        public List<Reservation> GetReservationsByDate(DateTime date)
        {
            return GetWhere(o => o.Date.Date == date && !o.IsDeleted).ToList();
        }

        public List<Reservation> GetReservationsByDate(DateTime date, ReservationConfirmationStatus status)
        {
            return GetWhere(o => o.ConfirmationStatus == status && o.Date.Date == date && !o.IsDeleted).ToList();
        }

        public ReservationStatsViewModel GetReservationStats()
        {
            var todayreservations = GetWhere(o => o.Date.Date == DateTime.Today && o.ConfirmationStatus != Core.Domain.Common.ReservationConfirmationStatus.Pending, false);
            var countreservations = todayreservations.Count();
            var countreservationpeople = todayreservations.Sum(o => o.NumberOfPeople);

            var yesterdayreservations = GetWhere(o => o.Date.Date == DateTime.Today.AddDays(-1) && o.ConfirmationStatus != Core.Domain.Common.ReservationConfirmationStatus.Pending, false);
            var yesterdaycountreservations = yesterdayreservations.Count();
            var yesterdaycountreservationpeople = yesterdayreservations.Sum(o => o.NumberOfPeople);
            ReservationStatsViewModel rm = new ReservationStatsViewModel
            {
                TodayReservations = countreservations,
                TodayReservationPeople = countreservationpeople,
                YesterdayReservations = yesterdaycountreservations,
                YesterdayReservationPeople = yesterdaycountreservationpeople
            };
            return rm;
        }
    }
}
