using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblMenuCategory", Schema = "menu")]
    public class MenuCategory : BaseEntity
    {
        [Key]
        public Guid MenuCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid HeaderPhotoID { get; set; }
        public string HeaderPhotoURL { get; set; }
        public Guid LogoPhotoID { get; set; }
        public string LogoPhotoURL { get; set; }
        public bool Active { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
