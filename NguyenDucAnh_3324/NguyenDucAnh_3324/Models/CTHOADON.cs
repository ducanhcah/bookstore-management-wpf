using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDucAnh_3324.Models
{
    [Table("CTHOADON")]
    public class CTHOADON
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string MaHD { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string MaSach { get; set; }

        [ForeignKey("MaHD")]
        public virtual HOADON HOADON { get; set; }

        [ForeignKey("MaSach")]
        public virtual SACH SACH { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }
    }
}
