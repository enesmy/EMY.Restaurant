using EMY.Papel.Restaurant.Core.Domain.Common;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public Guid BasketID { get; set; }
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
        public List<BasketItem> BasketItems { get; set; }
    }
}
