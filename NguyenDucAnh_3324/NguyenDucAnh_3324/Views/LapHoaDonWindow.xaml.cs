using System.Windows;
using NguyenDucAnh_3324.ViewModels;

namespace NguyenDucAnh_3324.Views
{
    public partial class LapHoaDonWindow : Window
    {
        public LapHoaDonWindow()
        {
            InitializeComponent();
            DataContext = new LapHoaDonViewModel();
        }
    }
}
