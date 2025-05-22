using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiDanhSachHieuXePage : ContentView
{
	public readonly QuanLiDanhSachHieuXePageViewModel _viewModel; 
	public QuanLiDanhSachHieuXePage(QuanLiDanhSachHieuXePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel; 
	}
}