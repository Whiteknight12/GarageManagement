using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class SuaTaiKhoanPage : ContentView
{
	public readonly SuaTaiKhoanPageViewModel _viewModel;
	
	public SuaTaiKhoanPage(SuaTaiKhoanPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}