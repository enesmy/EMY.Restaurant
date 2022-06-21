using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Domain.ViewModels
{
    public class ReservationPageResultViewModel
    {
        public List<Reservation> AuthorizedReservations { get; set; }
        public List<Reservation> UnAuthorizedReservations { get; set; }
        public List<Reservation> Pendings { get; set; }
        public DateTime End { get; set; }
        public DateTime Begin { get; set; }
    }
}
