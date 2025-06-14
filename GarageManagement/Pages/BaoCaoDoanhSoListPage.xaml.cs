using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class BaoCaoDoanhSoListPage : ContentView
{
	public readonly BaoCaoDoanhSoListPageViewModel _viewModel; 
	public BaoCaoDoanhSoListPage(BaoCaoDoanhSoListPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel; 
	}
}