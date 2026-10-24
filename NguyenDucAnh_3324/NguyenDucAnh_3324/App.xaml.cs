using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NguyenDucAnh_3324
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // WPF (khác ASP.NET) không tự hiểu |DataDirectory| là gì.
            // Dòng này khai báo |DataDirectory| trỏ về thư mục chứa file .exe đang chạy
            // (bin/Debug), đúng nơi file QL_NHASACH.mdf được copy tới khi build.
            // Nếu thiếu dòng này, chương trình sẽ báo lỗi không tìm thấy file .mdf.
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
