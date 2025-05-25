using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiPhieuThuTienPage : ContentView
{
    public readonly QuanLiPhieuThuTienPageViewModel _viewModel;
    public QuanLiPhieuThuTienPage(QuanLiPhieuThuTienPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
}