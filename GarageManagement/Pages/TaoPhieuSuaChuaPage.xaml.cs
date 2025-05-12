using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GarageManagement.Pages;

public partial class TaoPhieuSuaChuaPage : ContentPage
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

	private void OnVTPTChanged(object sender, EventArgs e)
	{
		if (sender is Picker picker && picker.SelectedItem is VatTuPhuTung vtpt)
		{
			if (picker.BindingContext is ChiTietPhieuSuaChua noidung)
			{
				noidung.VatTuPhuTungId = vtpt.VatTuPhuTungId;
				noidung.DonGia = vtpt.DonGiaBanLoaiVatTuPhuTung;
                if (noidung.TienCongId != null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
                {
                    var tienCong = _viewmodel.ListTienCong.FirstOrDefault(u => u.Id == noidung.TienCongId);
                    noidung.ThanhTien = (noidung.SoLuong * vtpt.DonGiaBanLoaiVatTuPhuTung) + tienCong.DonGiaLoaiTienCong;
                    noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
					_viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
                }
                noidung.OnPropertyChanged(nameof(noidung.DonGia));
			}
		}
	}

	private async void OnSoLuongChanged(object sender, EventArgs e)
	{
		if (sender is Entry entry && entry.BindingContext is ChiTietPhieuSuaChua noidung)
		{
            if (noidung.TienCongId != null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
			{
                var item = await _vatTuService.GetByID(noidung.VatTuPhuTungId ?? Guid.Empty);
                var tienCong = _viewmodel.ListTienCong.FirstOrDefault(u => u.Id == noidung.TienCongId);
                noidung.ThanhTien = (noidung.SoLuong * item.DonGiaBanLoaiVatTuPhuTung) + tienCong.DonGiaLoaiTienCong;
				noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
				_viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
			}
		}
	}

	private async void OnTienCongChanged(object sender, EventArgs e)
	{
        if (sender is Picker picker && picker.BindingContext is ChiTietPhieuSuaChua noidung && picker.SelectedItem is TienCong tiencong)
        {
            if (noidung.TienCongId!=null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
			{
                var item = await _vatTuService.GetByID(noidung.VatTuPhuTungId ?? Guid.Empty);
                noidung.ThanhTien = (noidung.SoLuong * item.DonGiaBanLoaiVatTuPhuTung) + tiencong.DonGiaLoaiTienCong;
				noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
				_viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
			}
		}
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		_=_viewmodel.LoadAsync();
    }
}