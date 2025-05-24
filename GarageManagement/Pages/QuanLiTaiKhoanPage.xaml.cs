using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiTaiKhoanPage : ContentView
{
	public readonly QuanLiTaiKhoanPageViewModel _viewModel;
	
	public QuanLiTaiKhoanPage(QuanLiTaiKhoanPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}