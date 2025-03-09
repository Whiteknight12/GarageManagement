using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class NhanSuMainPage : ContentPage
{
	public NhanSuMainPage()
	{
		InitializeComponent();
		BindingContext = new NhanSuMainPageViewModel();
	}
}