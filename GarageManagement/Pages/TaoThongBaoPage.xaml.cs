using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class TaoThongBaoPage : ContentView
{
	public readonly TaoThongBaoPageViewModel _viewModel;
	
	public TaoThongBaoPage(TaoThongBaoPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}