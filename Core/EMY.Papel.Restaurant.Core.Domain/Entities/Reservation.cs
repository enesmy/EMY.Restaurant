using EMY.Papel.Restaurant.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblReservations", Schema = "reservation")]
    public class Reservation : BaseEntity
    {
        [Key]
        public Guid ReservationID { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfPeople { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public string Message { get; set; }
        public ReservationConfirmationStatus ConfirmationStatus { get; set; }
    }
}
