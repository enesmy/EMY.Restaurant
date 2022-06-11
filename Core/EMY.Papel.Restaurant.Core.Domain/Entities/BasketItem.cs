using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblBasketItem", Schema = "basket")]
    public class BasketItem : BaseEntity
    {
        [Key]
        public Guid BasketItemID { get; set; }
        public Guid BasketID { get; set; }
        [ForeignKey("BasketID")] public Basket Basket { get; set; }
        public Guid MenuID { get; set; }
        public string MenuText { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemPrice { get; set; }
        public virtual Decimal TotalPrice { get { return Math.Round(ItemCount * ItemPrice, 2); } }

    }
}
