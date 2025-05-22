using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThuTienPage : ContentView
{
	private readonly APIClientService<Xe> _carservice;
	private readonly APIClientService<HieuXe> _hieuxeService;
    private readonly ThuTienPageViewModel _viewmodel;
	public ThuTienPage(ThuTienPageViewModel viewmodel,
		APIClientService<Xe> carservice,
        APIClientService<HieuXe> hieuxeService)
	{ 
		_viewmodel = viewmodel;
        BindingContext = _viewmodel;
        _carservice = carservice;
        _hieuxeService = hieuxeService;
        InitializeComponent();
	}

	private async void OnChuXeChanged(object sender, EventArgs e)
	{
		var sdt = _viewmodel.SelectedChuXe.SoDienThoai;
		var list = await _carservice.GetListOnSpecialRequirement($"PhoneNumber/{sdt}");
		if (list is not null)
		{
			_viewmodel.ListBienSo.Clear();
			foreach (var item in list) _viewmodel.ListBienSo.Add(item);
		}
		_viewmodel.DienThoai = sdt;
		_viewmodel.Email = _viewmodel.SelectedChuXe.Email;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		_ = _viewmodel.LoadAsync();
    }
	private async void OnSelectedBienSoIndexChanged(object sender, EventArgs e)
	{
		if(sender is Picker picker)
		{
             var hieuXe = await _hieuxeService.GetByID(_viewmodel.SelectedBienSo.HieuXeId);

			_viewmodel.TenHieuXe = hieuXe.TenHieuXe;
		}
	}
}