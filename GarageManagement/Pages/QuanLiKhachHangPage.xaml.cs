using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiKhachHangPage : ContentView
{
    public readonly QuanLiKhachHangPageViewModel _viewModel;
    public QuanLiKhachHangPage(QuanLiKhachHangPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
}