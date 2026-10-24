using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using NguyenDucAnh_3324.Models;

namespace NguyenDucAnh_3324.ViewModels
{
    public class LapHoaDonViewModel : ViewModelBase
    {
        private QL_NHASACHEntities db = new QL_NHASACHEntities();

        public ObservableCollection<SACH> DsSach { get; set; }
        public ObservableCollection<DongHoaDon> DsChiTiet { get; set; }

        private string _maHD;
        public string MaHD { get => _maHD; set => SetProperty(ref _maHD, value); }

        private string _tenKH;
        public string TenKH { get => _tenKH; set => SetProperty(ref _tenKH, value); }

        private DateTime? _ngayLap = DateTime.Now;
        public DateTime? NgayLap { get => _ngayLap; set => SetProperty(ref _ngayLap, value); }

        private SACH _sachChonThem;
        public SACH SachChonThem
        {
            get => _sachChonThem;
            set
            {
                SetProperty(ref _sachChonThem, value);
                if (value != null)
                    DonGiaThem = value.DonGia;  
            }
        }

        private int _soLuongThem = 1;
        public int SoLuongThem { get => _soLuongThem; set => SetProperty(ref _soLuongThem, value); }

        private decimal _donGiaThem;
        public decimal DonGiaThem { get => _donGiaThem; set => SetProperty(ref _donGiaThem, value); }

        private decimal _tongTien;
        public decimal TongTien { get => _tongTien; set => SetProperty(ref _tongTien, value); }

        private DongHoaDon _dongDangChon;
        public DongHoaDon DongDangChon { get => _dongDangChon; set => SetProperty(ref _dongDangChon, value); }

        public RelayCommand ThemDongCommand { get; set; }
        public RelayCommand XoaDongCommand { get; set; }
        public RelayCommand LuuHoaDonCommand { get; set; }

        public LapHoaDonViewModel()
        {
            DsSach = new ObservableCollection<SACH>(db.SACHs.ToList());
            DsChiTiet = new ObservableCollection<DongHoaDon>();

            ThemDongCommand = new RelayCommand(ThemDong, dk => SachChonThem != null && SoLuongThem > 0);
            XoaDongCommand = new RelayCommand(XoaDong, dk => DongDangChon != null);
            LuuHoaDonCommand = new RelayCommand(LuuHoaDon, dk => DsChiTiet.Count > 0 && !string.IsNullOrWhiteSpace(MaHD));
        }

        private void ThemDong(object tso)
        {
            if (SachChonThem.SoLuong < SoLuongThem)
            {
                MessageBox.Show("Số lượng tồn kho không đủ");
                return;
            }

            var dongCu = DsChiTiet.FirstOrDefault(x => x.MaSach == SachChonThem.MaSach);
            if (dongCu != null)
            {
                dongCu.SoLuong += SoLuongThem;
                int idx = DsChiTiet.IndexOf(dongCu);
                DsChiTiet.RemoveAt(idx);
                DsChiTiet.Insert(idx, dongCu);
            }
            else
            {
                DsChiTiet.Add(new DongHoaDon
                {
                    MaSach = SachChonThem.MaSach,
                    TenSach = SachChonThem.TenSach,
                    SoLuong = SoLuongThem,
                    DonGia = DonGiaThem
                });
            }

            TinhTongTien();
        }

        private void XoaDong(object tso)
        {
            if (DongDangChon == null) return;
            DsChiTiet.Remove(DongDangChon);
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            TongTien = DsChiTiet.Sum(x => x.ThanhTien);
        }

        private void LuuHoaDon(object tso)
        {
            try
            {
                HOADON hd = new HOADON
                {
                    MaHD = MaHD.Trim(),
                    NgayLap = NgayLap ?? DateTime.Now,
                    TenKH = TenKH
                };
                db.HOADONs.Add(hd);

                foreach (var dong in DsChiTiet)
                {
                    db.CTHOADONs.Add(new CTHOADON
                    {
                        MaHD = hd.MaHD,
                        MaSach = dong.MaSach,
                        SoLuong = dong.SoLuong,
                        DonGia = dong.DonGia
                    });

                    var sach = db.SACHs.FirstOrDefault(x => x.MaSach == dong.MaSach);
                    if (sach != null) sach.SoLuong -= dong.SoLuong;  
                }

                db.SaveChanges();
                MessageBox.Show("Lưu hoá đơn thành công. Tổng tiền: " + TongTien.ToString("N0") + " đ");

                DsChiTiet.Clear();
                TongTien = 0;
                MaHD = null;
                TenKH = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trùng mã hoá đơn hoặc lỗi lưu dữ liệu: " + ex.Message);
            }
        }
    }
}
