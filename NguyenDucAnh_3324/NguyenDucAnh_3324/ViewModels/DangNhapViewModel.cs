using System;
using System.Linq;
using System.Windows;
using NguyenDucAnh_3324.Models;

namespace NguyenDucAnh_3324.ViewModels
{
    public class DangNhapViewModel : ViewModelBase
    {
        private QL_NHASACHEntities db = new QL_NHASACHEntities();

        private string _tenDangNhap;
        public string TenDangNhap
        {
            get => _tenDangNhap;
            set => SetProperty(ref _tenDangNhap, value);
        }
        public string MatKhau { get; set; }

        public event Action DangNhapThanhCong;

        public RelayCommand DangNhapCommand { get; set; }

        public DangNhapViewModel()
        {
            DangNhapCommand = new RelayCommand(DangNhap);
        }

        private void DangNhap(object tso)
        {
            if (string.IsNullOrWhiteSpace(TenDangNhap) || string.IsNullOrWhiteSpace(MatKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu");
                return;
            }

            var tk = db.TAIKHOANs.FirstOrDefault(x =>
                x.TenDN == TenDangNhap.Trim() && x.MatKhau == MatKhau);

            if (tk != null)
            {
                DangNhapThanhCong?.Invoke();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }
    }
}
