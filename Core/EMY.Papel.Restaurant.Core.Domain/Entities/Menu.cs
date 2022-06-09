using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblMenu", Schema = "menu")]
    public class Menu : BaseEntity
    {
        [Key]
        public Guid MenuID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public Guid MenuCategoryID { get; set; }
        [ForeignKey("MenuCategoryID")] public virtual MenuCategory Category { get; set; }
        public Guid PhotoID { get; set; }
    }
}
