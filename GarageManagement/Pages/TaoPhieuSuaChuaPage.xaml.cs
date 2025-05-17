using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;
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
		if (sender is Picker picker && picker.SelectedItem is VatTuPhuTung item)
		{
            if (picker.BindingContext is VTPTChiTietPhieuSuaChua vtptList)
            {
                int count = _viewmodel.ListVTPT.Count(i => i.SelectedVTPTId == item.VatTuPhuTungId);
                if (count >= 2)
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
                vtptList.DonGia = item.DonGiaBanLoaiVatTuPhuTung;
                vtptList.OnPropertyChanged(nameof(vtptList.DonGia));
            }
		}
	}

	private async void OnSoLuongChanged(object sender, EventArgs e)
	{
        /*if (sender is Entry entry && entry.BindingContext is VTPTChiTietPhieuSuaChua vtptItem)
        {
            var noidung = _viewmodel.ListNoiDung.FirstOrDefault(nd => nd.Id == vtptItem.ChiTietPhieuSuaChuaId);
            if (noidung != null)
            {
                double tongThanhTienVTPT = noidung.ListVTPT.Sum(x => (x.SoLuong * (x.DonGia ?? 0)));
                double tienCong = noidung.GiaTienCong ?? 0;
                noidung.ThanhTien = tongThanhTienVTPT + tienCong;

                noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
                _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
            }
        }*/
    }

	private async void OnNoiDungSuaChuaChanged(object sender, EventArgs e)
	{
        /*if (sender is Picker picker && picker.BindingContext is ChiTietPhieuSuaChua noidung)
        {
            if (noidung.NoiDungSuaChuaId != Guid.Empty)
            {
                var tienCong = _viewmodel.ListTienCong.FirstOrDefault(u => u.NoiDungSuaChuaId == noidung.NoiDungSuaChuaId);
                noidung.TienCongId = tienCong?.Id;
                noidung.GiaTienCong = tienCong?.DonGiaLoaiTienCong;
                noidung.OnPropertyChanged(nameof(noidung.GiaTienCong));

                if (noidung.ListVTPT != null && noidung.ListVTPT.Any())
                {
                    double tongThanhTienVTPT = noidung.ListVTPT.Sum(x => (x.SoLuong * (x.DonGia ?? 0)));
                    noidung.ThanhTien = tongThanhTienVTPT + (tienCong?.DonGiaLoaiTienCong ?? 0);
                }
                else
                {
                    noidung.ThanhTien = tienCong?.DonGiaLoaiTienCong ?? 0;
                }

                noidung.OnPropertyChanged(nameof(noidung.ThanhTien));
                _viewmodel.TongThanhTien = _viewmodel.ListNoiDung.Sum(u => u.ThanhTien ?? 0);
            }
        }*/
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