using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThemLoaiTienCongPage : ContentPage
{
    public ThemLoaiTienCongPage(ThemLoaiTienCongPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}