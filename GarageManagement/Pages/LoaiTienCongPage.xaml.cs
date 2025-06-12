using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LoaiTienCongPage : ContentView
{
    //private readonly LoaiTienCongPageViewModel _viewModel;
    public LoaiTienCongPage(LoaiTienCongPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}