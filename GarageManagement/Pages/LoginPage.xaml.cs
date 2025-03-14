using GarageManagement.Services;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(AuthenticationService service)
	{
		InitializeComponent();
        BindingContext=new LoginPageViewModel(service);
    }
    public void OnRegisterTapped(object sender, EventArgs e)
    {
        
    }
}