using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThuTienPage : ContentPage
{
	private readonly APIClientService<Car> _carservice;
	private readonly ThuTienPageViewModel _viewmodel;
	public ThuTienPage(ThuTienPageViewModel viewmodel,
		APIClientService<Car> carservice)
	{ 
		_viewmodel = viewmodel;
        BindingContext = _viewmodel;
        _carservice = carservice;
		InitializeComponent();
	}
	private async void OnChuXeChanged(object sender, EventArgs e)
	{
		var sdt = _viewmodel.SelectedChuXe.PhoneNumber;
		var list = await _carservice.GetListOnSpecialRequirement($"GetListThroughPhoneNumber/{sdt}");
		if (list is not null)
		{
			_viewmodel.ListBienSo.Clear();
			foreach (var item in list) _viewmodel.ListBienSo.Add(item);
		}
		_viewmodel.DienThoai = sdt;
		_viewmodel.Email = _viewmodel.SelectedChuXe.Email;
	}
}