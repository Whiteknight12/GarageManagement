using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThuTienPage : ContentView
{
	private readonly APIClientService<Xe> _carservice;
	private readonly APIClientService<HieuXe> _hieuxeService;
    public readonly ThuTienPageViewModel _viewmodel;
	private List<HieuXe> hieuXes = new List<HieuXe>();
    public ThuTienPage(ThuTienPageViewModel viewmodel,
		APIClientService<Xe> carservice,
        APIClientService<HieuXe> hieuxeService)
	{ 
		_viewmodel = viewmodel;
        BindingContext = _viewmodel;
        _carservice = carservice;
        _hieuxeService = hieuxeService;
        InitializeComponent();
		_ = set(); 
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
		_viewmodel.Email = _viewmodel.SelectedChuXe.Email ??"";
		_viewmodel.SelectedBienSo = _viewmodel.ListBienSo[0];
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		_ = _viewmodel.LoadAsync();
    }
	private async void OnSelectedBienSoIndexChanged(object sender, EventArgs e)
	{
		if(sender is Picker picker && _viewmodel.available == false)
		{
			foreach(var hieuXe in hieuXes)
			{
				if(hieuXe.Id == _viewmodel.SelectedBienSo.HieuXeId)
				{
                    _viewmodel.TenHieuXe = hieuXe.TenHieuXe ?? "";
					_viewmodel.TienNoXeSelected = _viewmodel.SelectedBienSo.TienNo.ToString() ?? ""; 
					break; 
                }
			}
		}
	}
	private async Task set()
	{
        hieuXes = await _hieuxeService.GetAll();
    }
}