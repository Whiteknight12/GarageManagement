using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiPhieuSuaChuaPage : ContentView
{
    public readonly QuanLiPhieuSuaChuaPageViewModel _viewModel;
    public QuanLiPhieuSuaChuaPage(QuanLiPhieuSuaChuaPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
}