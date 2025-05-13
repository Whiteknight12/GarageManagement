
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using GarageManagement.ViewModels;
using System.Collections.ObjectModel;

namespace GarageManagement.Pages;

public partial class LapPhieuNhapPage : ContentPage
{
    LapPhieuNhapPageViewModel _viewModel;
    public LapPhieuNhapPage(LapPhieuNhapPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private void OnSoLuongChanged(object sender, EventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is ChiTietPhieuNhapVatTu chiTietPhieuNhapVatTu)
        {
            if (chiTietPhieuNhapVatTu.SoLuong != null && chiTietPhieuNhapVatTu.DonGia != null)
            {
                var listGiaTien = _viewModel.ListChiTietPhieuNhap.Select(c => c.SoLuong * c.DonGia);
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
                var listGiaTien = _viewModel.ListChiTietPhieuNhap.Select(c => c.SoLuong * c.DonGia);
                _viewModel.TongGiaTien = listGiaTien.Sum(l => l.Value);
            }
        }
    }
    private void OnLoaiVatTuPicked(object sender, EventArgs e)
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
            }
            //cập nhật lại tránh bị trùng 
            _viewModel.listChiTietId = _viewModel.ListChiTietPhieuNhap
                .Where(item => item.VatTuId != null) // Loại bỏ các mục có VatTuId = null
                .Select(item => (Guid)item.VatTuId)  // Chỉ ép kiểu những mục không null
                .ToList();
        }
    }

}