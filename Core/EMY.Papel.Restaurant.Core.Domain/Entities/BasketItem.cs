namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketItemID { get; set; }
        public Guid BasketID { get; set; }
        public Guid MenuID { get; set; }
        public string MenuText { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemPrice { get; set; }
        public virtual Decimal TotalPrice { get { return Math.Round(ItemCount * ItemPrice, 2); } }

    }
}
