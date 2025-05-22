using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThemHieuXePage : ContentPage
{
	public readonly ThemHieuXePageViewModel _viewModel;
    public ThemHieuXePage(ThemHieuXePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }
}