using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class MainPage : ContentPage
{	
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}