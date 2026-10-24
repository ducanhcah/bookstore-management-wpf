using System.Windows;
using NguyenDucAnh_3324.ViewModels;

namespace NguyenDucAnh_3324.Views
{
    public partial class DangNhapWindow : Window
    {
        private DangNhapViewModel vm;

        public DangNhapWindow()
        {
            InitializeComponent();
            vm = new DangNhapViewModel();
            DataContext = vm;
            vm.DangNhapThanhCong += Vm_DangNhapThanhCong;
        }

        private void Vm_DangNhapThanhCong()
        {
            QuanLySachWindow w = new QuanLySachWindow();
            w.Show();
            this.Close();
        }

        private void pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.MatKhau = pwd.Password;
        }
    }
}
