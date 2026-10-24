using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDucAnh_3324.Models
{
    [Table("TAIKHOAN")]
    public class TAIKHOAN
    {
        [Key]
        [StringLength(50)]
        public string TenDN { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }
    }
}
