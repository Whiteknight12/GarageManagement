using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class TiepNhanXePage : ContentView
{
	public readonly TiepNhanXePageViewModel _viewModel;
	
	public TiepNhanXePage(APIClientService<PhieuTiepNhan> service, 
		APIClientService<ThamSo> ruleService, 
		APIClientService<Xe> carService,
		APIClientService<HieuXe> hieuxeService,
		APIClientService<KhachHang> userService,
		APIClientService<NhomNguoiDung> groupService,
		TiepNhanXePageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

	private void OnBienSoChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBienSo.Text))
        {
			_viewModel.IsCarNotFound = false;
        }
    }
}