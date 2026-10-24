using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDucAnh_3324.Models
{
    [Table("THELOAI")]
    public class THELOAI
    {
        [Key]
        [StringLength(10)]
        public string MaTL { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTL { get; set; }

        public virtual ICollection<SACH> SACHs { get; set; }
    }
}
