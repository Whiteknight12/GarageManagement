using GarageManagement.Services;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LoginPage : ContentPage
{
    private bool _isPassword = true;
    public LoginPage(AuthenticationService service)
	{
		InitializeComponent();
        BindingContext=new LoginPageViewModel(service);
    }

    public void OnRegisterTapped(object sender, EventArgs e)
    {
        
    }

    private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
    {
        _isPassword = !_isPassword;
        PasswordEntry.IsPassword = _isPassword;
    }

}