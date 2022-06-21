using EMY.Papel.Restaurant.Core.Domain.Common;
using EMY.Papel.Restaurant.Core.Domain.Entities;
using EMY.Papel.Restaurant.Core.Domain.ViewModels;

namespace EMY.Papel.Restaurant.Core.Application.Repositories.ReservationRepositories
{
    public interface IReservationReadRepository : IReadRepository<Reservation>
    {
        ReservationStatsViewModel GetReservationStats();
        List<Reservation> GetReservations();
        List<Reservation> GetAuthorizedReservations();
        List<Reservation> GetReservationsByDate(DateTime date);
        List<Reservation> GetReservationsByDate(DateTime date, ReservationConfirmationStatus status);
    }
}
