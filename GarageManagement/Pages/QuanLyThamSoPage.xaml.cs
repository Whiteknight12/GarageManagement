using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLyThamSoPage : ContentView
{
	public QuanLyThamSoPage(QuanLyThamSoPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}