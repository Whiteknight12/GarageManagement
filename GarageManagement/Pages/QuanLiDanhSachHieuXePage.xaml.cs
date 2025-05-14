using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiDanhSachHieuXePage : ContentPage
{
	//private readonly QuanLiDanhSachHieuXePageViewModel _viewModel; 
	public QuanLiDanhSachHieuXePage(QuanLiDanhSachHieuXePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel; 
	}
}