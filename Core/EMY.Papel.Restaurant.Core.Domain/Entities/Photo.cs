using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Papel.Restaurant.Core.Domain.Entities
{
    [Table("tblPhoto", Schema = "dbo")]
    public class Photo : BaseEntity
    {

        [Key]
        public Guid PhotoID { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }


        public virtual string GetOrginalLocation
        {
            get
            {
                return Path.Combine(SystemStatics.GetCurrentPhotoArchiveLocation(), PhotoID + "." + Extention);
            }
        }

        public virtual string GetThumbnailLocation
        {
            get
            {
                return Path.Combine(SystemStatics.GetCurrentPhotoThumbnailLocation(), PhotoID + "." + Extention);
            }
        }
    }
}
