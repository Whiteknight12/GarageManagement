using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LichSuPage : ContentView
{
	public readonly LichSuPageViewModel _viewModel;
	
	public LichSuPage(LichSuPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}