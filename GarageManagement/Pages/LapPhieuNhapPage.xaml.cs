
using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LapPhieuNhapPage : ContentView
{
    private bool _isDarkMode = false;
    private readonly APIClientService<VatTuPhuTung> _vatTuService; 
    public readonly LapPhieuNhapPageViewModel _viewModel;
    public LapPhieuNhapPage(LapPhieuNhapPageViewModel viewModel,
        APIClientService<VatTuPhuTung> vatTuService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _vatTuService = vatTuService; 
    }

    private void OnSoLuongChanged(object sender, EventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is ChiTietPhieuNhapVatTu chiTietPhieuNhapVatTu)
        {
            if (chiTietPhieuNhapVatTu.SoLuong != null && chiTietPhieuNhapVatTu.DonGia != null)
            {
                var listGiaTien = _viewModel.ListChiTietPhieuNhap.Where(c => c.SoLuong != null && c.DonGia != null).Select(c => c.SoLuong * c.DonGia);
                _viewModel.TongGiaTien = listGiaTien.Sum(l => l.Value);
            }
        }
    }
    private void OnDonGiaChanged(object sender, EventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is ChiTietPhieuNhapVatTu chiTietPhieuNhapVatTu)
        {
            if (chiTietPhieuNhapVatTu.SoLuong != null && chiTietPhieuNhapVatTu.DonGia != null)
            {
                var listGiaTien = _viewModel.ListChiTietPhieuNhap.Where(c => c.SoLuong != null && c.DonGia != null).Select(c => c.SoLuong * c.DonGia);
                _viewModel.TongGiaTien = listGiaTien.Sum(l => l.Value);
            }
        }
    }   
    private async void OnLoaiVatTuPicked(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.BindingContext is ChiTietPhieuNhapVatTu chiTietPhieuNhapVatTu)
        {
            //if (_viewModel.listChiTietId.Any(id => id == chiTietPhieuNhapVatTu.VatTuId))
            //{
            //    _ = Application.Current.MainPage.DisplayAlert(
            //        "Lỗi",
            //        "Không thể nhập cùng loại vật liệu trong 2 chi tiết phiếu nhập.",
            //        "OK");
            //}
            //else
            //{
            //    _viewModel.listChiTietId.Add(chiTietPhieuNhapVatTu.VatTuId);
            //}

            if (_viewModel.listChiTietId.Any(id => id == chiTietPhieuNhapVatTu.VatTuId))
            {
                _ = Application.Current.MainPage.DisplayAlert(
                    "Lỗi",
                    $"Vật tư đã được nhập, hãy chọn loại khác",
                    "OK");
                chiTietPhieuNhapVatTu.VatTuId = null;
                chiTietPhieuNhapVatTu.OnPropertyChanged(nameof(chiTietPhieuNhapVatTu.VatTuId));
            }
            else if (chiTietPhieuNhapVatTu.VatTuId != null)
            {
                _viewModel.listChiTietId.Add(chiTietPhieuNhapVatTu.VatTuId.Value);
                var vt = await _vatTuService.GetByID(chiTietPhieuNhapVatTu.VatTuId.Value);
                chiTietPhieuNhapVatTu.DonGia = vt?.DonGiaBanLoaiVatTuPhuTung ?? 0;
                chiTietPhieuNhapVatTu.OnPropertyChanged(nameof(chiTietPhieuNhapVatTu.DonGia));
            }
            //cập nhật lại tránh bị trùng 
            _viewModel.listChiTietId = _viewModel.ListChiTietPhieuNhap
                .Where(item => item.VatTuId != null) // Loại bỏ các mục có VatTuId = null
                .Select(item => (Guid)item.VatTuId)  // Chỉ ép kiểu những mục không null
                .ToList();
        }
    }

    //private void OnToggleThemeClicked(object sender, EventArgs e)
    //{
    //    // Toggle the theme state
    //    _isDarkMode = !_isDarkMode;

    //    // Update the resources dynamically based on the theme state
    //    Resources["BackgroundColor"] = _isDarkMode ? (Color)Resources["BackgroundColorDark"] : (Color)Resources["BackgroundColorLight"];
    //    Resources["CardBackgroundColor"] = _isDarkMode ? (Color)Resources["CardBackgroundColorDark"] : (Color)Resources["CardBackgroundColorLight"];
    //    Resources["TextColor"] = _isDarkMode ? (Color)Resources["TextColorDark"] : (Color)Resources["TextColorLight"];
    //    Resources["InputBackgroundColor"] = _isDarkMode ? (Color)Resources["InputBackgroundColorDark"] : (Color)Resources["InputBackgroundColorLight"];
    //    Resources["PlaceholderColor"] = _isDarkMode ? (Color)Resources["PlaceholderColorDark"] : (Color)Resources["PlaceholderColorLight"];
    //}


}