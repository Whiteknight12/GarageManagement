using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class TiepNhanXePage : ContentPage
{
	public TiepNhanXePage(APIClientService<PhieuTiepNhan> service, APIClientService<ThamSo> ruleService, 
		APIClientService<Xe> carService,
		APIClientService<HieuXe> hieuxeService,
		APIClientService<KhachHang> userService,
		APIClientService<NhomNguoiDung> groupService)
	{
		InitializeComponent();
		BindingContext = new TiepNhanXePageViewModel(service, ruleService, carService, hieuxeService, userService, groupService);
	}
}