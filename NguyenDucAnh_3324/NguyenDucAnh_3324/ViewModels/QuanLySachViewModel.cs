using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using NguyenDucAnh_3324.Models;
using NguyenDucAnh_3324.Views;

namespace NguyenDucAnh_3324.ViewModels
{
    public class QuanLySachViewModel : ViewModelBase
    {
        private QL_NHASACHEntities db = new QL_NHASACHEntities();

        public ObservableCollection<SACH> DsSach { get; set; }
        public ObservableCollection<THELOAI> DsTheLoai { get; set; }

        private ICollectionView _view;
        public ICollectionView ViewDsSach => _view;

        private string _maSach;
        public string MaSach { get => _maSach; set => SetProperty(ref _maSach, value); }

        private string _tenSach;
        public string TenSach { get => _tenSach; set => SetProperty(ref _tenSach, value); }

        private string _tacGia;
        public string TacGia { get => _tacGia; set => SetProperty(ref _tacGia, value); }

        private DateTime? _ngayNhap = DateTime.Now;
        public DateTime? NgayNhap { get => _ngayNhap; set => SetProperty(ref _ngayNhap, value); }

        private int _soLuong;
        public int SoLuong { get => _soLuong; set => SetProperty(ref _soLuong, value); }

        private decimal _donGia;
        public decimal DonGia { get => _donGia; set => SetProperty(ref _donGia, value); }

        private THELOAI _theLoaiDangChon;
        public THELOAI TheLoaiDangChon
        {
            get => _theLoaiDangChon;
            set => SetProperty(ref _theLoaiDangChon, value);
        }

        private THELOAI _theLoaiLoc;
        public THELOAI TheLoaiLoc
        {
            get => _theLoaiLoc;
            set
            {
                SetProperty(ref _theLoaiLoc, value);
                _view?.Refresh();  
            }
        }

        private string _tuKhoaTim;
        public string TuKhoaTim
        {
            get => _tuKhoaTim;
            set => SetProperty(ref _tuKhoaTim, value);
        }

        private SACH _sachDangChon;
        public SACH SachDangChon
        {
            get => _sachDangChon;
            set
            {
                SetProperty(ref _sachDangChon, value);
                DoDuLieuLenForm();
            }
        }

        private string _ketQuaDem;
        public string KetQuaDem { get => _ketQuaDem; set => SetProperty(ref _ketQuaDem, value); }

        public RelayCommand ThemCommand { get; set; }
        public RelayCommand SuaCommand { get; set; }
        public RelayCommand XoaCommand { get; set; }
        public RelayCommand TimCommand { get; set; }
        public RelayCommand DemCommand { get; set; }
        public RelayCommand LamMoiCommand { get; set; }
        public RelayCommand MoLapHoaDonCommand { get; set; }

        public QuanLySachViewModel()
        {
            ThemCommand = new RelayCommand(Them);
            SuaCommand = new RelayCommand(Sua, dk => SachDangChon != null);
            XoaCommand = new RelayCommand(Xoa, dk => SachDangChon != null);
            TimCommand = new RelayCommand(Tim);
            DemCommand = new RelayCommand(Dem);
            LamMoiCommand = new RelayCommand(LamMoi);
            MoLapHoaDonCommand = new RelayCommand(MoLapHoaDon);

            LoadDsTheLoai();
            LoadDsSach();
        }

        private void LoadDsTheLoai()
        {
            DsTheLoai = new ObservableCollection<THELOAI>(db.THELOAIs.ToList());
            OnPropertyChanged(nameof(DsTheLoai));
            if (DsTheLoai.Count > 0)
                TheLoaiDangChon = DsTheLoai[0];
        }

        private void LoadDsSach()
        {
            DsSach = new ObservableCollection<SACH>(db.SACHs.ToList());
            OnPropertyChanged(nameof(DsSach));

            _view = CollectionViewSource.GetDefaultView(DsSach);
            _view.Filter = LocSach;
        }

        private bool LocSach(object obj)
        {
            SACH s = obj as SACH;
            if (s == null) return false;

            bool dungTheLoai = TheLoaiLoc == null || s.MaTL == TheLoaiLoc.MaTL;
            bool dungTuKhoa = string.IsNullOrWhiteSpace(TuKhoaTim)
                               || s.TenSach.ToLower().Contains(TuKhoaTim.Trim().ToLower());

            return dungTheLoai && dungTuKhoa;
        }

        private void DoDuLieuLenForm()
        {
            if (SachDangChon == null) return;
            MaSach = SachDangChon.MaSach;
            TenSach = SachDangChon.TenSach;
            TacGia = SachDangChon.TacGia;
            NgayNhap = SachDangChon.NgayNhap;
            SoLuong = SachDangChon.SoLuong;
            DonGia = SachDangChon.DonGia;
            TheLoaiDangChon = DsTheLoai.FirstOrDefault(x => x.MaTL == SachDangChon.MaTL);
        }

        private void Them(object tso)
        {
            if (string.IsNullOrWhiteSpace(MaSach) || string.IsNullOrWhiteSpace(TenSach))
            {
                MessageBox.Show("Vui lòng nhập Mã sách và Tên sách");
                return;
            }

            SACH s = null;
            try
            {
                s = new SACH
                {
                    MaSach = MaSach.Trim(),
                    TenSach = TenSach,
                    TacGia = TacGia,
                    MaTL = TheLoaiDangChon?.MaTL,
                    NgayNhap = NgayNhap,
                    SoLuong = SoLuong,
                    DonGia = DonGia
                };
                db.SACHs.Add(s);
                db.SaveChanges();
                DsSach.Add(s);
                MessageBox.Show("Thêm sách thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Trùng mã sách, vui lòng nhập mã khác");
                if (s != null) db.Entry(s).State = EntityState.Detached;
            }
        }

        private void Sua(object tso)
        {
            var s = db.SACHs.FirstOrDefault(x => x.MaSach == MaSach.Trim());
            if (s == null)
            {
                MessageBox.Show("Không tìm thấy sách để sửa");
                return;
            }

            s.TenSach = TenSach;
            s.TacGia = TacGia;
            s.MaTL = TheLoaiDangChon?.MaTL;
            s.NgayNhap = NgayNhap;
            s.SoLuong = SoLuong;
            s.DonGia = DonGia;
            db.SaveChanges();

            LoadDsSach();
            MessageBox.Show("Sửa thành công");
        }

        private void Xoa(object tso)
        {
            var s = db.SACHs.FirstOrDefault(x => x.MaSach == MaSach.Trim());
            if (s == null)
            {
                MessageBox.Show("Không tìm thấy sách để xoá");
                return;
            }

            var kq = MessageBox.Show($"Xoá sách '{s.TenSach}'?", "Xác nhận", MessageBoxButton.YesNo);
            if (kq != MessageBoxResult.Yes) return;

            db.SACHs.Remove(s);
            db.SaveChanges();
            DsSach.Remove(DsSach.First(x => x.MaSach == s.MaSach));
            MessageBox.Show("Xoá thành công");
            LamMoi(null);
        }

        private void Tim(object tso)
        {
            _view.Refresh();
        }

        private void Dem(object tso)
        {
            KetQuaDem = _view.Cast<SACH>().Count().ToString();
        }

        private void LamMoi(object tso)
        {
            MaSach = null; TenSach = null; TacGia = null;
            NgayNhap = DateTime.Now;
            SoLuong = 0; DonGia = 0;
            SachDangChon = null;
        }

        private void MoLapHoaDon(object tso)
        {
            LapHoaDonWindow w = new LapHoaDonWindow();
            w.Show();
        }
    }
}
