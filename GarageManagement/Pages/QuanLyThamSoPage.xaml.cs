using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLyThamSoPage : ContentView
{
	public readonly QuanLyThamSoPageViewModel _viewModel; 
    public QuanLyThamSoPage(QuanLyThamSoPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;

    }
}