using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChinhSuaPhieuNhapPage : ContentView
{
    private readonly ChinhSuaPhieuNhapPageViewModel _vm;
    private readonly Window _window;

    public ChinhSuaPhieuNhapPage(ChinhSuaPhieuNhapPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
      
        // Khi cập nhật xong thì tự động đóng cửa sổ
        _vm.OnUpdateSuccess = () => Application.Current?.CloseWindow(_window);
    }

    //public ChinhSuaPhieuNhapPage(ChinhSuaPhieuNhapPageViewModel vm, Window window)
    //    : this(vm)
    //{
    //    _window = window;
    //}

    private void OnLoaiVatTuPicked(object sender, EventArgs e)
    {
        RecalculateTotal();
    }

    // Khi số lượng hoặc đơn giá thay đổi
    private void OnValueChanged(object sender, TextChangedEventArgs e)
    {
        RecalculateTotal();
    }

    // Hàm chung để tính lại tổng giá
    private void RecalculateTotal()
    {
        var sum = _vm.ListChiTietPhieuNhap
            .Where(x => x.SoLuong != null && x.DonGia != null)
            .Sum(x => (x.SoLuong.Value * x.DonGia.Value));
        _vm.TongGiaTien = sum;
    }
}
