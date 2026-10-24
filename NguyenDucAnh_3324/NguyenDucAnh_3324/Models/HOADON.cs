using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDucAnh_3324.Models
{
    [Table("HOADON")]
    public class HOADON
    {
        [Key]
        [StringLength(10)]
        public string MaHD { get; set; }

        [Required]
        public DateTime NgayLap { get; set; }

        [StringLength(100)]
        public string TenKH { get; set; }

        public virtual ICollection<CTHOADON> CTHOADONs { get; set; }
    }
}
