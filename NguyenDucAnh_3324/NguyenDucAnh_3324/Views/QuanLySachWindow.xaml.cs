using System.Windows;
using NguyenDucAnh_3324.ViewModels;

namespace NguyenDucAnh_3324.Views
{
    public partial class QuanLySachWindow : Window
    {
        public QuanLySachWindow()
        {
            InitializeComponent();
            DataContext = new QuanLySachViewModel();
        }


    }
}
