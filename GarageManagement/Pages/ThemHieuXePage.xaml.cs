using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThemHieuXePage : ContentPage
{
	public ThemHieuXePage(ThemHieuXePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}