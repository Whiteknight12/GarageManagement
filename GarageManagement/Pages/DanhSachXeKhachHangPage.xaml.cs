using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class DanhSachXeKhachHangPage : ContentView
{
	public readonly DanhSachXeKhachHangPageViewModel _viewModel;

	public DanhSachXeKhachHangPage(DanhSachXeKhachHangPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}