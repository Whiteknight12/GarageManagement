using APIClassLibrary.APIModels;
using GarageManagement.Services;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class SuaPhieuSuaChuaPage : ContentView
{
	public readonly SuaPhieuSuaChuaPageViewModel _viewModel;
	
	public SuaPhieuSuaChuaPage(SuaPhieuSuaChuaPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (Parent != null)
        {
            await _viewModel.LoadAsync();
            VTPTIdConverter.VatTuList = _viewModel.ListVatTuPhuTung;
            NDSCIdConverter.NoiDungList = _viewModel.ListNoiDungSuaChua;
        }
    }

    private async void OnVTPTChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedItem is VatTuPhuTung vtpt)
        {
            if (picker.BindingContext is VTPTChiTietPhieuSuaChua vtptList)
            {
                var chiTiet = _viewModel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptList.IdForUI);
                var item = chiTiet?.ListSpecifiedVTPT?.Where(u => u.SelectedVTPTId == vtpt.VatTuPhuTungId);
                if (item?.Count() >= 2)
                {
                    await Shell.Current.DisplayAlert("Error", "Vật tư phụ tùng đã được chọn trước đó", "OK");
                    vtptList.SelectedVTPTId = null;
                    vtptList.OnPropertyChanged(nameof(vtptList.SelectedVTPTId));
                    vtptList.DonGia = null;
                    vtptList.SoLuong = 0;
                    vtptList.OnPropertyChanged(nameof(vtptList.SoLuong));
                    vtptList.OnPropertyChanged(nameof(vtptList.DonGia));
                    return;
                }
                vtptList.DonGia = vtpt.DonGiaBanLoaiVatTuPhuTung;
                vtptList.OnPropertyChanged(nameof(vtptList.DonGia));
                var updateItem = _viewModel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptList.IdForUI);
                if (updateItem is not null)
                {
                    updateItem.ThanhTien = updateItem?.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia);
                    updateItem.ThanhTien += updateItem.GiaTienCong ?? 0;
                    updateItem.OnPropertyChanged(nameof(updateItem.ThanhTien));
                }
                _viewModel.TongThanhTien = _viewModel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
            }
        }
    }

    private async void OnSoLuongChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is VTPTChiTietPhieuSuaChua vtptItem)
        {
            if (vtptItem != null)
            {
                string newText = e.NewTextValue;
                if (int.TryParse(newText, out int soLuong))
                {
                    if (vtptItem.SelectedVTPTId != null)
                    {
                        int c = 0;
                        foreach (var item in _viewModel.ListNoiDung)
                        {
                            c += item?.ListSpecifiedVTPT?.FirstOrDefault(u => u.SelectedVTPTId == vtptItem.SelectedVTPTId)?.SoLuong ?? 0;
                        }
                        if (c > _viewModel.ListVatTuPhuTung?.FirstOrDefault(u => u.VatTuPhuTungId == vtptItem.SelectedVTPTId)?.SoLuong)
                        {
                            await Shell.Current.DisplayAlert("Thông báo", "Số lượng vật tư phụ tùng không đủ", "OK");
                            vtptItem.SoLuong = 0;
                            vtptItem.OnPropertyChanged(nameof(vtptItem.SoLuong));
                            return;
                        }
                    }
                    vtptItem.SoLuong = soLuong;
                    if (_viewModel.ListNoiDung != null && _viewModel.ListNoiDung.Any())
                    {
                        var itemNoiDung = _viewModel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptItem.IdForUI);
                        if (itemNoiDung != null)
                        {
                            itemNoiDung.ThanhTien = 0;
                            itemNoiDung.ThanhTien += itemNoiDung.GiaTienCong ?? 0;
                            itemNoiDung.ThanhTien += itemNoiDung?.ListSpecifiedVTPT?.Sum(u => (u.SoLuong * u.DonGia));
                            itemNoiDung?.OnPropertyChanged(nameof(itemNoiDung.ThanhTien));
                        }
                        _viewModel.TongThanhTien = _viewModel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
                    }
                }
                else
                {
                    if (_viewModel.ListNoiDung != null && _viewModel.ListNoiDung.Any())
                    {
                        var itemNoiDung = _viewModel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptItem.IdForUI);
                        if (itemNoiDung != null)
                        {
                            itemNoiDung.ListSpecifiedVTPT.FirstOrDefault(u => u.SelectedVTPTId == vtptItem.SelectedVTPTId).SoLuong = 0;
                            itemNoiDung.ThanhTien = 0;
                            itemNoiDung.ThanhTien += itemNoiDung.GiaTienCong ?? 0;
                            itemNoiDung.ThanhTien += itemNoiDung?.ListSpecifiedVTPT?.Sum(u => (u.SoLuong * u.DonGia));
                            itemNoiDung?.OnPropertyChanged(nameof(itemNoiDung.ThanhTien));
                        }
                    }
                    _viewModel.TongThanhTien = _viewModel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
                }
            }
        }
    }

    private async void OnNoiDungSuaChuaChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.BindingContext is ChiTietPhieuSuaChua noiDung)
        {
            var tmpList = _viewModel.ListNoiDung.Where(u => u.NoiDungSuaChuaId == noiDung.NoiDungSuaChuaId);
            if (tmpList.Count() >= 2)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Nội dung sữa chữa đã được chọn trước đó", "OK");
                noiDung.NoiDungSuaChuaId = null;
                noiDung.TienCongId = null;
                noiDung.GiaTienCong = null;
                noiDung.OnPropertyChanged(nameof(noiDung.GiaTienCong));
                noiDung.OnPropertyChanged(nameof(noiDung.NoiDungSuaChuaId));
                return;
            }
            var tienCong = _viewModel.ListTienCong.FirstOrDefault(u => u.NoiDungSuaChuaId == noiDung.NoiDungSuaChuaId);
            noiDung.TienCongId = tienCong?.Id;
            noiDung.GiaTienCong = tienCong?.DonGiaLoaiTienCong;
            noiDung.OnPropertyChanged(nameof(noiDung.GiaTienCong));
            var item = _viewModel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == noiDung.NoiDungId);
            if (item is not null)
            {
                item.ThanhTien = item.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia) + (noiDung.GiaTienCong ?? 0);
                item.OnPropertyChanged(nameof(item.ThanhTien));
            }
            _viewModel.TongThanhTien = _viewModel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }
    }
}