using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NguyenDucAnh_3324.ViewModels
{
    /// <summary>
    /// Lớp nền cho tất cả các ViewModel.
    /// Implement INotifyPropertyChanged để View (XAML) tự động cập nhật
    /// khi dữ liệu trong ViewModel thay đổi (Binding hai chiều).
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gọi hàm này trong setter của mỗi property để báo cho UI biết
        /// giá trị đã thay đổi.
        /// [CallerMemberName] giúp tự lấy tên property, không cần truyền tay.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string tenProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tenProperty));
        }

        /// <summary>
        /// Hàm set giá trị "gọn" cho property: tự so sánh giá trị cũ - mới,
        /// nếu khác thì gán và tự động raise PropertyChanged.
        /// Cách dùng trong ViewModel con:
        ///
        /// private string _hoTen;
        /// public string HoTen
        /// {
        ///     get => _hoTen;
        ///     set => SetProperty(ref _hoTen, value);
        /// }
        /// </summary>
        protected bool SetProperty<T>(ref T bienLuuTru, T giaTriMoi, [CallerMemberName] string tenProperty = null)
        {
            if (Equals(bienLuuTru, giaTriMoi))
                return false;

            bienLuuTru = giaTriMoi;
            OnPropertyChanged(tenProperty);
            return true;
        }
    }
}