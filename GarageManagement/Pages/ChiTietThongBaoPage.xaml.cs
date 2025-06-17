using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChiTietThongBaoPage : ContentView
{
	public readonly ChiTietThongBaoPageViewModel _viewModel;
	
	public ChiTietThongBaoPage(ChiTietThongBaoPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}