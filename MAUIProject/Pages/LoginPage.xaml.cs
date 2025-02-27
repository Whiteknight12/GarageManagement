using MAUIProject.Services;
using MAUIProject.ViewModels;

namespace MAUIProject.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(AuthenticationService authenticationService)
	{
		InitializeComponent();
        BindingContext = new LoginPageViewModel(authenticationService);
    }
	public async void CreateAccountClicked(Object sender, EventArgs e)
	{
        
    }
	public void ForgotPasswordClicked(Object sender, EventArgs e)
    {

    }
	public void Login(object sender, EventArgs e)
    {

    }
}