using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChiTietPhieuSuaChuaPage : ContentView
{
	public ChiTietPhieuSuaChuaPageViewModel _viewModel;
	
	public ChiTietPhieuSuaChuaPage(ChiTietPhieuSuaChuaPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}