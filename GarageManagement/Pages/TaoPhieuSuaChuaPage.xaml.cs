using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;

namespace GarageManagement.Pages;

public partial class TaoPhieuSuaChuaPage : ContentView
{
	private readonly TaoPhieuSuaChuaPageViewModel _viewmodel;
	private readonly APIClientService<VatTuPhuTung> _vatTuService;

	public TaoPhieuSuaChuaPage(APIClientService<Xe> carservice,
		APIClientService<TienCong> congservice,
		APIClientService<VatTuPhuTung> vattuservice,
		APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
		APIClientService<PhieuSuaChua> phieuservice,
		TaoPhieuSuaChuaPageViewModel viewmodel,
		APIClientService<VatTuPhuTung> vatTuService)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		_viewmodel = viewmodel;
        _vatTuService = vatTuService;
    }

	private async void OnVTPTChanged(object sender, EventArgs e)
	{
        if (sender is Picker picker && picker.SelectedItem is VatTuPhuTung vtpt)
        {
            if (picker.BindingContext is VTPTChiTietPhieuSuaChua vtptList)
            {
                var chiTiet=_viewmodel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptList.IdForUI);
                var item = chiTiet?.ListSpecifiedVTPT?.Where(u => u.SelectedVTPTId == vtpt.VatTuPhuTungId);
                if (item?.Count()>=2)
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
                var updateItem=_viewmodel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptList.IdForUI);
                if (updateItem is not null)
                {
                    updateItem.ThanhTien = updateItem?.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia);
                    updateItem.ThanhTien += updateItem.GiaTienCong ?? 0;
                    updateItem.OnPropertyChanged(nameof(updateItem.ThanhTien));
                }
                _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
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
                    if (vtptItem.SelectedVTPTId!=null)
                    {
                        int c = 0;
                        foreach (var item in _viewmodel.ListNoiDung)
                        {
                            c+=item?.ListSpecifiedVTPT?.FirstOrDefault(u => u.SelectedVTPTId == vtptItem.SelectedVTPTId)?.SoLuong??0;
                        }
                        if (c>_viewmodel.ListVatTuPhuTung?.FirstOrDefault(u=>u.VatTuPhuTungId==vtptItem.SelectedVTPTId)?.SoLuong)
                        {
                            await Shell.Current.DisplayAlert("Thông báo", "Số lượng vật tư phụ tùng không đủ", "OK");
                            vtptItem.SoLuong = 0;
                            vtptItem.OnPropertyChanged(nameof(vtptItem.SoLuong));
                            return;
                        }
                    }
                    vtptItem.SoLuong = soLuong;
                    if (_viewmodel.ListNoiDung != null && _viewmodel.ListNoiDung.Any())
                    {
                        var itemNoiDung = _viewmodel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptItem.IdForUI);
                        if (itemNoiDung != null)
                        {
                            itemNoiDung.ThanhTien = 0;
                            itemNoiDung.ThanhTien += itemNoiDung.GiaTienCong ?? 0;
                            itemNoiDung.ThanhTien += itemNoiDung?.ListSpecifiedVTPT?.Sum(u => (u.SoLuong * u.DonGia));
                            itemNoiDung?.OnPropertyChanged(nameof(itemNoiDung.ThanhTien));
                        }
                        _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
                    }
                }
                else
                {
                    if (_viewmodel.ListNoiDung != null && _viewmodel.ListNoiDung.Any())
                    {
                        var itemNoiDung = _viewmodel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == vtptItem.IdForUI);
                        if (itemNoiDung != null)
                        {
                            itemNoiDung.ListSpecifiedVTPT.FirstOrDefault(u => u.SelectedVTPTId == vtptItem.SelectedVTPTId).SoLuong = 0;
                            itemNoiDung.ThanhTien = 0;
                            itemNoiDung.ThanhTien += itemNoiDung.GiaTienCong ?? 0;
                            itemNoiDung.ThanhTien += itemNoiDung?.ListSpecifiedVTPT?.Sum(u => (u.SoLuong * u.DonGia));
                            itemNoiDung?.OnPropertyChanged(nameof(itemNoiDung.ThanhTien));
                        }
                    }
                    _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
                }
            }
        }
    }

	private async void OnNoiDungSuaChuaChanged(object sender, EventArgs e)
	{
        if (sender is Picker picker && picker.BindingContext is ChiTietPhieuSuaChua noiDung)
        {
            var tmpList=_viewmodel.ListNoiDung.Where(u => u.NoiDungSuaChuaId == noiDung.NoiDungSuaChuaId);
            if (tmpList.Count()>=2)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Nội dung sữa chữa đã được chọn trước đó", "OK");
                noiDung.NoiDungSuaChuaId = null;
                noiDung.TienCongId = null;
                noiDung.GiaTienCong = null;
                noiDung.OnPropertyChanged(nameof(noiDung.GiaTienCong));
                noiDung.OnPropertyChanged(nameof(noiDung.NoiDungSuaChuaId));
                return; 
            }
            var tienCong = _viewmodel.ListTienCong.FirstOrDefault(u => u.NoiDungSuaChuaId == noiDung.NoiDungSuaChuaId);
            noiDung.TienCongId = tienCong?.Id;
            noiDung.GiaTienCong = tienCong?.DonGiaLoaiTienCong;
            noiDung.OnPropertyChanged(nameof(noiDung.GiaTienCong));
            var item=_viewmodel.ListNoiDung.FirstOrDefault(u => u.NoiDungId == noiDung.NoiDungId);
            if (item is not null)
            {
                item.ThanhTien=item.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia) + (noiDung.GiaTienCong ?? 0);
                item.OnPropertyChanged(nameof(item.ThanhTien));
            }
            _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		if (width > 0 && height > 0)
		{
			_ = _viewmodel.LoadAsync();
		}
    }
}