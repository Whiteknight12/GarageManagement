using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiPhieuTiepNhanXePage : ContentView
{
	public QuanLiPhieuTiepNhanPageViewModel _viewModel;
	
	public QuanLiPhieuTiepNhanXePage(QuanLiPhieuTiepNhanPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;

		MessagingCenter.Subscribe<ChiTietKhachHangViewModel, Guid>(
			this, "ReloadCustomerList", (sender, Id) =>
			{
				_=_viewModel.LoadAsync();
            });
    }
}