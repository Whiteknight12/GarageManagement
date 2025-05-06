using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GarageManagement.Pages;

public partial class TaoPhieuSuaChuaPage : ContentPage
{
	private readonly TaoPhieuSuaChuaPageViewModel _viewmodel;
	public TaoPhieuSuaChuaPage(APIClientService<Xe> carservice,
		APIClientService<TienCong> congservice,
		APIClientService<VatTuPhuTung> vattuservice,
		APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
		APIClientService<PhieuSuaChua> phieuservice,
		TaoPhieuSuaChuaPageViewModel viewmodel
		)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		_viewmodel = viewmodel;
	}
	private void OnVTPTChanged(object sender, EventArgs e)
	{
		if (sender is Picker picker && picker.SelectedItem is VatTuPhuTung vtpt)
		{
			if (picker.BindingContext is ChiTietPhieuSuaChua noidung)
			{
				noidung.DonGia = vtpt.DonGiaBanLoaiVatTuPhuTung;
                if (noidung.TienCongId != null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
                {
                    var tc = _viewmodel.listtiencong.Where(u => u.Id == noidung.TienCongId).FirstOrDefault();
                    noidung.ThanhTien = (noidung.SoLuong * noidung.DonGia) + tc.DonGiaLoaiTienCong;
                    noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
					_viewmodel.Tongthanhtien = _viewmodel.Listnoidung.Sum(u => u.ThanhTien ?? 0);
                }
                noidung.OnPropertyChanged(nameof(noidung.DonGia));
			}
		}
	}
	private void OnSoLuongChanged(object sender, EventArgs e)
	{
		if (sender is Entry entry && entry.BindingContext is ChiTietPhieuSuaChua noidung)
		{
			if (noidung.TienCongId != null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
			{
				var tc = _viewmodel.listtiencong.Where(u => u.Id == noidung.TienCongId).FirstOrDefault();
				noidung.ThanhTien = (noidung.SoLuong * noidung.DonGia) + tc.DonGiaLoaiTienCong;
				noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
                _viewmodel.Tongthanhtien = _viewmodel.Listnoidung.Sum(u => u.ThanhTien ?? 0);
            }
		}
	}
	private void OnTienCongChanged(object sender, EventArgs e)
	{
        if (sender is Picker picker && picker.BindingContext is ChiTietPhieuSuaChua noidung && picker.SelectedItem is TienCong tiencong)
        {
            if (noidung.TienCongId != null && noidung.SoLuong != null && noidung.VatTuPhuTungId != null)
            {
				noidung.ThanhTien = (noidung.SoLuong * noidung.DonGia) + tiencong.DonGiaLoaiTienCong;
                noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
                _viewmodel.Tongthanhtien = _viewmodel.Listnoidung.Sum(u => u.ThanhTien ?? 0);
            }
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (BindingContext is TaoPhieuSuaChuaPageViewModel vm) vm.LoadAsync();
    }
}