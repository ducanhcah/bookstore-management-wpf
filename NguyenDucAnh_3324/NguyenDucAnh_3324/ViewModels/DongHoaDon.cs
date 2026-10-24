namespace NguyenDucAnh_3324.ViewModels
{
    public class DongHoaDon
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
