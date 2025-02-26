using MAUIProject.ViewModels;

namespace MAUIProject.Pages;

public partial class LoginPage : ContentPage
{
	private CreateAccountPage createaccountpage;
	public LoginPage(UserViewModel viewmodel, CreateAccountPage createaccountpage)
	{
		InitializeComponent();
        BindingContext = viewmodel;
        this.createaccountpage = createaccountpage;
    }
	public async void CreateAccountClicked(Object sender, EventArgs e)
	{
        Navigation.PushAsync(createaccountpage);
    }
	public void ForgotPasswordClicked(Object sender, EventArgs e)
    {

    }
	public void Login(object sender, EventArgs e)
    {

    }
}