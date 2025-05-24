using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class SuaHieuXePage : ContentView
{
	public SuaHieuXePageViewModel _viewModel;
	
	public SuaHieuXePage(SuaHieuXePageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}