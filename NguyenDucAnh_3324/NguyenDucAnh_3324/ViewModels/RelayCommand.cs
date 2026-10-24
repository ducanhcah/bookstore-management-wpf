using System;
using System.Windows.Input;

namespace NguyenDucAnh_3324.ViewModels
{
    /// <summary>
    /// RelayCommand thay thế cho sự kiện Click trong code-behind.
    /// Cho phép Button trong XAML bind trực tiếp tới 1 hàm trong ViewModel:
    ///
    ///     <Button Content="Thêm" Command="{Binding ThemCommand}"/>
    ///
    /// thay vì:
    ///
    ///     <Button Content="Thêm" Click="Them"/>
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _hanhDong;       
        private readonly Predicate<object> _dieuKienChay;  

        /// <summary>
        /// Constructor không cần điều kiện: nút luôn bấm được.
        /// vd: ThemCommand = new RelayCommand(Them);
        /// </summary>
        public RelayCommand(Action<object> hanhDong) : this(hanhDong, null) { }

        /// <summary>
        /// Constructor có điều kiện: nút chỉ bấm được khi dieuKienChay trả về true.
        /// vd: SuaCommand = new RelayCommand(Sua, canExecute => SachDangChon != null);
        /// </summary>
        public RelayCommand(Action<object> hanhDong, Predicate<object> dieuKienChay)
        {
            _hanhDong = hanhDong ?? throw new ArgumentNullException(nameof(hanhDong));
            _dieuKienChay = dieuKienChay;
        }

        public bool CanExecute(object tham_so)
        {
            return _dieuKienChay == null || _dieuKienChay(tham_so);
        }

        public void Execute(object tham_so)
        {
            _hanhDong(tham_so);
        }

        /// <summary>
        /// WPF tự lắng nghe sự kiện này để biết khi nào cần gọi lại CanExecute
        /// (vd: nút Sửa/Xoá tự bật/tắt khi người dùng chọn dòng khác trong DataGrid).
        /// Gắn vào CommandManager để không phải tự raise thủ công.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Gọi hàm này nếu muốn ép WPF kiểm tra lại CanExecute ngay lập tức
        /// (thường không cần, vì CommandManager đã tự làm).
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}