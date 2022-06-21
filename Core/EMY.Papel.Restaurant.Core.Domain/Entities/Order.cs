using EMY.Papel.Restaurant.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblOrder", Schema = "order")]
    public class Order : BaseEntity
    {
        [Key]
        public Guid OrderID { get; set; }
        public string FullName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullAdress { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public BasketAuthorizeStatus IsAuthorized { get; set; }
        public decimal Discount { get; set; }
        public decimal RealPrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public DateTime AuthorizeDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentAuthorizationToken { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CCV { get; set; }
    }
}
