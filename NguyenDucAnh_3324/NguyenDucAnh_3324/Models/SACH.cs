using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDucAnh_3324.Models
{
    [Table("SACH")]
    public class SACH
    {
        [Key]
        [StringLength(10)]
        public string MaSach { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSach { get; set; }

        [StringLength(10)]
        public string MaTL { get; set; }

        [ForeignKey("MaTL")]
        public virtual THELOAI THELOAI { get; set; }

        [StringLength(100)]
        public string TacGia { get; set; }

        public DateTime? NgayNhap { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }
    }
}
