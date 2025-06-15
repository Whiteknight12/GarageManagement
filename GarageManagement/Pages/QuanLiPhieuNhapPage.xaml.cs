using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiPhieuNhapPage : ContentView
{
    public readonly QuanLiPhieuNhapPageViewModel _viewModel;
    public QuanLiPhieuNhapPage(QuanLiPhieuNhapPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        
    }
}